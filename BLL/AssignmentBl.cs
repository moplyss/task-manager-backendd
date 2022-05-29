using BLL.DTO;
using BLL.Interfaces;
using DAL.Model;
using DAL.Repositories.Interfaces;

namespace BLL;

public class AssignmentBL: IAssignmentBl
{
    private readonly IUnitOfWork _work;

    public AssignmentBL(IUnitOfWork unitOfWork)
    {
        _work = unitOfWork;
    }

    public List<AssignmentDTO> GetAllAssignments() =>
        _work.AssignmentRepository
        .Read()
        .ToList()
        .ConvertAll(a => App.Mapper.Map<AssignmentDTO>(a));

    public List<AssignmentDTO> FindAssignment(string searchText) =>
        _work.AssignmentRepository
        .Filter(a => a.Title.Contains(searchText) || a.Description.Contains(searchText))
        .ToList()
        .ConvertAll(a => App.Mapper.Map<AssignmentDTO>(a));

    public AssignmentDTO CreateAssignment(string title, string description, int projectId)
    {
        Assignment assignment = new()
        {
            Title = title,
            Description = description,
            Status = Status.Created,
            ProjectId = projectId
        };

        _work.AssignmentRepository.Create(assignment);
        _work.Save();

        return App.Mapper.Map<AssignmentDTO>(assignment);
    }

    public AssignmentDTO UpdateAssignment(int id, string title, string description, string status)
    {
        Assignment? assignment = _work.AssignmentRepository.Find(a => a.Id == id);

        if (assignment is null)
            throw new ArgumentException();

        assignment.Title = title;
        assignment.Description = description;
        assignment.Status = (Status) Enum.Parse(typeof(Status), status);

        _work.AssignmentRepository.Update(assignment);
        _work.Save();

        return App.Mapper.Map<AssignmentDTO>(assignment);
    }

    public bool DeleteAssignment(int assignmentId)
    {
        Assignment? assignment = _work.AssignmentRepository.Find(a => a.Id == assignmentId);

        if (assignment is null)
            return false;

        _work.AssignmentRepository.Delete(assignment);
        _work.Save();

        return true;
    }

    public string[] GetPossibleStatuses() => Enum.GetNames(typeof(Status));
}

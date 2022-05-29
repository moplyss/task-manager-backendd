using BLL.DTO;

namespace BLL.Interfaces;

public interface IAssignmentBl
{
    public List<AssignmentDTO> GetAllAssignments();

    public List<AssignmentDTO> FindAssignment(string searchText);

    public AssignmentDTO CreateAssignment(string title, string description, int projectId);

    public AssignmentDTO UpdateAssignment(int id, string title, string description, string status);

    public bool DeleteAssignment(int assignmentId);

    public string[] GetPossibleStatuses();
}

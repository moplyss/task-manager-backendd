using BLL.DTO;
using BLL.Interfaces;
using DAL.Model;
using DAL.Repositories.Interfaces;
using DAL.Repository;

namespace BLL;

public class ProjectBL: IProjectBl
{
    private readonly IUnitOfWork _work;

    public ProjectBL(IUnitOfWork unitOfWork)
    {
        _work = unitOfWork;
    }

    public List<ProjectDTO> GetAllProjects() =>
        _work.ProjectRepository
        .Read()
        .ToList()
        .ConvertAll(p => App.Mapper.Map<ProjectDTO>(p));

    public List<ProjectDTO> FindProject(string searchText) =>
        _work.ProjectRepository
        .Filter(p => p.Title.Contains(searchText) || p.Description.Contains(searchText))
        .ToList()
        .ConvertAll(p => App.Mapper.Map<ProjectDTO>(p));

    public ProjectDTO CreateProject(string title, string description)
    {
        Project project = new()
        {
            Title = title,
            Description = description
        };

        _work.ProjectRepository.Create(project);
        _work.Save();

        return App.Mapper.Map<ProjectDTO>(project);
    }

    public ProjectDTO UpdateProject(int projectId, string title, string description)
    {
        Project? project = _work.ProjectRepository.Find(p => p.Id == projectId);

        if (project is null)
            throw new ArgumentException();

        project.Title = title;
        project.Description = description;

        _work.ProjectRepository.Update(project);
        _work.Save();

        return App.Mapper.Map<ProjectDTO>(project);
    }

    public bool DeleteProject(int projectId)
    {
        Project? project = _work.ProjectRepository.Find(p => p.Id == projectId);

        if (project is null)
            return false;

        _work.ProjectRepository.Delete(project);
        _work.Save();

        return true;
    }
}

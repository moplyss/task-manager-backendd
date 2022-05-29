using BLL.DTO;

namespace BLL.Interfaces;

public interface IProjectBl
{
    public List<ProjectDTO> GetAllProjects();

    public List<ProjectDTO> FindProject(string searchText);

    public ProjectDTO CreateProject(string title, string description);


    public ProjectDTO UpdateProject(int projectId, string title, string description);

    public bool DeleteProject(int projectId);
}

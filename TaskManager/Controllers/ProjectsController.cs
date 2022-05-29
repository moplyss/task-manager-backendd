using BLL;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    public readonly IProjectBl _service = ServiceLocator.GetService<IProjectBl>();

    [Route("[action]")]
    [HttpPut]
    public ProjectDTO Create(ProjectCreatingBody body) => _service.CreateProject(body.Title, body.Description);

    [Route("[action]")]
    [HttpGet]
    public IEnumerable<ProjectDTO> Read() => _service.GetAllProjects();

    [Route("[action]")]
    [HttpPatch]
    public ProjectDTO Update(ProjectUpdatingBody body) => _service.UpdateProject(body.Id, body.Title, body.Description);

    [Route("[action]")]
    [HttpDelete]
    public bool Delete(ProjectDeletingBody body) => _service.DeleteProject(body.Id);
}

public class ProjectDeletingBody
{
    public int Id { get; set; }
}

public class ProjectCreatingBody
{
    public string Title { get; set; } = "";

    public string Description { get; set; } = "";
}

public class ProjectUpdatingBody
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";
}

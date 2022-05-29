using BLL;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentBl _service = ServiceLocator.GetService<IAssignmentBl>();

    [Route("[action]")]
    [HttpPut]
    public AssignmentDTO Create(AssignmentCreatingBody body) => _service.CreateAssignment(body.Title, body.Description, body.ProjectId);

    [Route("[action]")]
    [HttpGet]
    public IEnumerable<AssignmentDTO> Read() => _service.GetAllAssignments();

    [Route("[action]")]
    [HttpGet]
    public IEnumerable<string> GetPossibleStatuses() => _service.GetPossibleStatuses();

    [Route("[action]")]
    [HttpPatch]
    public AssignmentDTO Update(AssignmentUpdatingBody body) => _service.UpdateAssignment(body.Id, body.Title, body.Description, body.Status);

    [Route("[action]")]
    [HttpDelete]
    public bool Delete(AssignmentDeletingBody body) => _service.DeleteAssignment(body.Id);


}

public class AssignmentDeletingBody
{
    public int Id { get; set; }
}

public class AssignmentCreatingBody
{
    public int ProjectId { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";
}

public class AssignmentUpdatingBody
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string Status { get; set; } = "";
}


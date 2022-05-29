namespace BLL.DTO;

public class AssignmentDTO
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public string Status { get; set; } = "";
}

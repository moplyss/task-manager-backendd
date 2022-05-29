using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model;

[Table("Assignments")]
public class Assignment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }
}

public enum Status
{
    Created,
    Doing,
    Finished
}
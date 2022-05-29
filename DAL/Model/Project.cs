using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model;

[Table("Projects")]
public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}
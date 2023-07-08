using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEsp32Master.Domain;

public class Tasks
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int? PersonId { get; set; }
    public virtual Person Person { get; set; }

    public int? GroupId { get; set; }
    public virtual Group Group { get; set; }

    public int? TagId { get; set; }
    public virtual Tag Tag { get; set; }

}

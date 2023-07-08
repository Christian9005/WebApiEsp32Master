using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiEsp32Master.Domain;

public class Tag
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CodeNumber { get; set; }

    [Required]
    public int CustomId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace WebApiEsp32Master.Domain.Dtos;

public class CreateGroupDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public List<int?> PeopleIds { get; set; }
}

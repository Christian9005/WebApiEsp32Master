using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiEsp32Master.Domain;

public class Person
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [JsonIgnore]
    public int? GroupId { get; set; }
}


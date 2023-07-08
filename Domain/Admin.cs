using System.ComponentModel.DataAnnotations;

namespace WebApiEsp32Master.Domain;

public class Admin
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
}

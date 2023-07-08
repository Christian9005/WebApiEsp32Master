using System.ComponentModel.DataAnnotations;

namespace WebApiEsp32Master.Domain;

public class Zona
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }
    
    public List<int> Datos { get; set; }

}

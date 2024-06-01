using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class VideogameCreateDTO
{
   [Required]
   [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 letras")]
   public string? Name { get; set; }
   [Required]
    public string? Genre { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public double Price {get; set;}
    [Required]
    public string? Developer { get; set; }
    [Required]
     public string? Platform { get; set; }
     [Required]
     public int Valoration { get; set; }
    
    
}
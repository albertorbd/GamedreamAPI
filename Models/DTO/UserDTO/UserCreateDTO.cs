using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class UserCreateDTO
{   
    [Required]
    [StringLength(20, ErrorMessage = "El nombre debe tener menos de 20 letras")]
    public string? Name { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "El nombre debe tener menos de 20 letras")]
    public string? Lastname { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
    public string? Email { get; set; }
     [Required]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }
    [Required]
    public string? DNI { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }

}
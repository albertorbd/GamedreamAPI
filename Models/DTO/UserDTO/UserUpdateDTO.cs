using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class UserUpdateDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
    public string? Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string? Password { get; set; }
    
}
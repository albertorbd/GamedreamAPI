using System.ComponentModel.DataAnnotations;

public class UserLoginDTO
{   
    [Required(ErrorMessage = "El email es obligatorio.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class MoneyTransferDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    public double Amount { get; set; }

    [StringLength(30, ErrorMessage = "El método de pago debe tener máximo 30 carácteres")]
    public string? Method { get; set; }
}
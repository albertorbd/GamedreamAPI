using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class BuyVideogameDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del Videojuego no es válido")]
    public int VideogameId { get; set; }

    
}
using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class OperationQueryParameters
{
    
    [Range(1, int.MaxValue, ErrorMessage = "El ID del videojuego no es válido")]
    public int? VideogameId { get; set; }


    [StringLength(25, ErrorMessage = "El concepto debe tener máximo 25 caracteres")]
    public string? Concept { get; set; }


}
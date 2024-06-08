using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;

public class VideogameQueryParameters
{
    [StringLength(30, ErrorMessage = "El nombre debe tener máximo 30 carácteres")]
    public string? Name { get; set; }

    [StringLength(20, ErrorMessage = "El género debe tener máximo 20 carácteres")]
    public string? Genre { get; set; }

    [StringLength(20, ErrorMessage = "El desarrollador debe tener máximo 20 carácteres")]
    public string? Developer { get; set; }

    [StringLength(15, ErrorMessage = "La plataforma debe tener máximo 15 carácteres")]
    public string? Platform { get; set; }
}
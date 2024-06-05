using System.ComponentModel.DataAnnotations;

namespace GamedreamAPI.Models;
public class Videogame
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Genre { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTime RegisterDate { get; set; }
    [Required]
    public double? Price {get; set;}
    [Required]
    public string? Developer { get; set; }
    [Required]
     public string? Platform { get; set; }
    [Required]
    public int? Valoration { get; set; }
    
    
    public static int VideogameIdSeed { get; set; }

    public Videogame() {}

    public Videogame (string name, string genre, string description, double price, string developer, string platform, int valoration){

    Id = VideogameIdSeed++;
    Name = name;
    Genre = genre;
    Description = description;
    RegisterDate= DateTime.Now;
    Price=price;
    Developer = developer;
    Platform= platform;
    Valoration = valoration;
    }
}

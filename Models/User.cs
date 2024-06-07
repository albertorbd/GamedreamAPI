using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamedreamAPI.Models;
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Lastname { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? DNI { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public double Money { get; set; }
    [JsonIgnore]
    public List<Operation> Operations { get; set; }
     [Required]
    public string Role { get; set; }  = Roles.User;

    
    
    

    public User() {}

    public User (string name, string lastname, string email, string password, string dni, DateTime birthdate){

    
    Name = name;
    Lastname = lastname;
    Email = email;
    Password= password;
    DNI = dni;
    BirthDate = birthdate;
    Money= 0.0;
    Operations= new List<Operation>();
    }
}
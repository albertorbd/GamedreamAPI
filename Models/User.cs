namespace GamedreamAPI.Models;
public class User
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? DNI { get; set; }
    public DateTime BirthDate { get; set; }
    public double Money { get; set; }
    public List<Operation> Operations { get; set; }
    public Dictionary<string, double> Videogames { get; set; }

    
    
    public static int UserIdSeed { get; set; }

    public User() {}

    public User (string name, string lastname, string email, string password, string dni, DateTime birthdate){

    Id = UserIdSeed++;
    Name = name;
    Lastname = lastname;
    Email = email;
    Password= password;
    DNI = dni;
    BirthDate = birthdate;
    Money= 0.0;
    Operations= new List<Operation>();
    Videogames= new Dictionary<string, double>();
    }
}
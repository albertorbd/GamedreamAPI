using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GamedreamAPI.Models;
public class Operation
{   
    [Key]
     public int Id { get; set; }
     [ForeignKey("User")]
     public int UserId { get; set; }
     //[ForeignKey("Videogame")]
     public int? VideogameId { get; set; }
     [Required]
     public string? Concept { get; set; }
     public DateTime Date { get; set; }
     public double Quantity { get; set; }
     public double Amount { get; set; }
     public string? Method { get; set; }
     [JsonIgnore]
     public User user { get; set; }
     [JsonIgnore]
     public Videogame videogame { get; set; }

    

    public Operation() {}


     public Operation (string concept, double amount, string method, int userId)
    {
        
        UserId=userId;    
        Concept = concept;
        Quantity=1;
        Date = DateTime.Now;
        Amount = amount;
        Method = method;
    }

    public Operation (int videogameId,  string concept,double price,int userId, int quantity)
    {
       
        UserId= userId;
        VideogameId = videogameId;
        Concept = concept;
        Date = DateTime.Now;
        Quantity=quantity;
        Method= null;
        
    }

}
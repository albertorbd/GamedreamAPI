namespace GamedreamAPI.Models;
public class Operation
{
    public int Id { get; set; }
    public Videogame Videogame { get; set; }
    public string? Concept { get; set; }
    public DateTime Date { get; set; }
    public double Quantity { get; set; }
    public double Amount { get; set; }
    public string? Method { get; set; }

    public static int IdOperationSeed;

    public Operation() {}


     public Operation (string concept, double amount, string method)
    {
        Id = IdOperationSeed++;
        Videogame = null;
        Concept = concept;
        Quantity=1;
        Date = DateTime.Now;
        Amount = amount;
        Method = method;
    }

    public Operation (Videogame videogame, string concept,double price)
    {
        Id = IdOperationSeed++;
        Videogame = videogame;
        Concept = concept;
        Date = DateTime.Now;
        Method= null;
         Quantity= 1;
        
    }

}
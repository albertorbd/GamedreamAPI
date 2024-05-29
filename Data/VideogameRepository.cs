using GamedreamAPI.Models;
using System.Text.Json;

namespace GamedreamAPI.Data;
public class VideogameRepository : IVideogameRepository
{
private Dictionary<string, Videogame> _videogame = new Dictionary<string, Videogame>();
   private readonly string _filePath =  "videogames.json";
    private readonly string _logsfilePath = "logs.json";
         
    public VideogameRepository()
    {
        LoadVideogames();
    }
    public void AddVideogame(Videogame videogame)
    {
         
        _videogame[videogame.Id.ToString()] = videogame;
    }
     
     public Dictionary<string,Videogame> GetAllVideogames(){
         return new Dictionary<string, Videogame>(_videogame);
     }

     public Videogame GetVideogame(string name){
        var allGames = GetAllVideogames();
            foreach (var videogames in allGames.Values)
            {
                if (videogames.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return videogames;
                }
            }
            
            return null;
     }
     
    public void DeleteVideogame(Videogame videogame){
    _videogame.Remove(videogame.Id.ToString());
    }

    public void UpdateVideogame(Videogame videogame){
        _videogame[videogame.Id.ToString()]= videogame;
    }

    public void LoadVideogames()
    {
     if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var videogames = JsonSerializer.Deserialize<IEnumerable<Videogame>>(jsonString);
                _videogame = videogames.ToDictionary(videogame => videogame.Id.ToString());

                int highestId = _videogame.Values.Max(v => v.Id);

            
                 Videogame.VideogameIdSeed = highestId + 1;

               
            }
            catch (Exception e)
            {
                LogError("Error searching videogames archive", e);
            }
        }
         

    }
    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_videogame.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            LogError("Error saving changes", e);
            throw new Exception("Error saving changes in Videogames archive", e);
        }
    }

    public void LogError(string message, Exception exception)
    {
        try
        {
            using (StreamWriter writer= new StreamWriter(_logsfilePath, true)){
                writer.WriteLine($"{DateTime.Now} ERROR {message}");
                writer.WriteLine(exception.ToString());
                writer.WriteLine();
            }
            
        }
        catch (Exception e)
        {
            throw new Exception("Error has ocurred writting in logs", e);
        }
    }

        
}

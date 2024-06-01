using GamedreamAPI.Models;

namespace GamedreamAPI.Data;

public interface IVideogameRepository
{
    void AddVideogame(Videogame videogame);
    IEnumerable<Videogame> GetAllVideogames();
    Videogame GetVideogame(string name);
    void DeleteVideogame(Videogame videogame);
    void UpdateVideogame(Videogame videogame);
    void SaveChanges();
    void LogError(string message, Exception exception);


}
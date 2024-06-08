using System.Reflection.Metadata;
using GamedreamAPI.Models;

namespace GamedreamAPI.Data;

public interface IVideogameRepository
{
    void AddVideogame(Videogame videogame);
    IEnumerable<Videogame> GetAllVideogames(VideogameQueryParameters videogameQueryParameters, bool orderByIdDesc);
    Videogame GetVideogameByName(string name);
    Videogame GetVideogameById(int id);
    void DeleteVideogame(Videogame videogame);
    void UpdateVideogame(Videogame videogame);
    void SaveChanges();
   


}
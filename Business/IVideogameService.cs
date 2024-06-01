using System.Reflection.PortableExecutable;
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IVideogameService
{
    public Videogame RegisterVideogame(VideogameCreateDTO videogameCreateDTO);
    public IEnumerable<Videogame> GetAllVideogames();
    bool CheckVideogame(string name);
    public Videogame GetVideogame(string name);
    void DeleteVideogame(string name);
    void UpdateVideogame(string videogameName, VideogameUpdateDTO videogameUpdateDTO);
    string InputEmpty();
    
}
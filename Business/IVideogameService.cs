
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IVideogameService
{
    public Videogame RegisterVideogame(VideogameCreateDTO videogameCreateDTO);
    public IEnumerable<Videogame> GetAllVideogames(VideogameQueryParameters videogameQueryParameters, bool orderByNameAsc);
    public Videogame GetVideogame(string name);
    void DeleteVideogame(string name);
    void UpdateVideogame(string videogameName, VideogameUpdateDTO videogameUpdateDTO);
    string InputEmpty();
    
}
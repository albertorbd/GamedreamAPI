
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IVideogameService
{
    public Videogame RegisterVideogame(VideogameCreateDTO videogameCreateDTO);
    public IEnumerable<Videogame> GetAllVideogames(VideogameQueryParameters videogameQueryParameters, bool orderByNameAsc);
    public Videogame GetVideogameByName(string videogameName);
    public Videogame GetVideogameById(int videogameId);
    void DeleteVideogame(int videogameId);
    void UpdateVideogame(int videogameId, VideogameUpdateDTO videogameUpdateDTO);
    string InputEmpty();
    
}
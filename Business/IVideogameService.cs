using System.Reflection.PortableExecutable;
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IVideogameService
{
   void RegisterVideogame(string name, string genre, string description, double price, string developer, string platform, int valoration);
    void PrintAllVideogames();
    bool CheckVideogame(string name);
    Videogame GetVideogame(string name);
    void DeleteVideogame(string name);
    void UpdateVideogame(Videogame videogame, string newGenre, string newDescription, string newDeveloper, string newPlatform, int newValoration);
    string InputEmpty();
    void SearchVideogameByName();
}
using System.Linq.Expressions;
using GamedreamAPI.Data;
using GamedreamAPI.Models;

namespace GamedreamAPI.Business;

public class VideogameService: IVideogameService
{
private readonly IVideogameRepository _repository;

public VideogameService(IVideogameRepository repository)
{

_repository=repository;

}


public Videogame RegisterVideogame( VideogameCreateDTO videogameCreateDTO){
 try
 {
    Videogame videogame= new(videogameCreateDTO.Name,videogameCreateDTO.Genre, videogameCreateDTO.Description, videogameCreateDTO.Price, videogameCreateDTO.Developer, videogameCreateDTO.Platform, videogameCreateDTO.Valoration);
    _repository.AddVideogame(videogame);
    _repository.SaveChanges();
    return videogame;
 }   
 catch(Exception e){
    _repository.LogError("Error registering the videogame", e);
    throw new Exception("An error ocurred registering the Videogame", e);
 }
}

public  IEnumerable<Videogame> GetAllVideogames(){
   
  return _repository.GetAllVideogames();
}

    public bool CheckVideogame(string name)
    {
        try
        {
            var videogames = _repository.GetAllVideogames();
            foreach (var videogame in videogames)
            {
                if (videogame.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error checking if videogame exist", e);
            throw new Exception("An error has ocurred checking the videogame", e);
        }
    }
    public Videogame GetVideogame(string name){
        try{
            return _repository.GetVideogame(name);
        }
        catch(Exception e){
            _repository.LogError("Error getting the videogame", e);
            throw new Exception("An error has ocurred getting the videogame", e);
        }
    }

    public void DeleteVideogame(string gamename){
         
        try{
           Videogame getVideogame = GetVideogame(gamename);

           if (getVideogame != null){
           _repository.DeleteVideogame(getVideogame);
           _repository.SaveChanges();
           Console.WriteLine("El videojuego ha sido borrado correctamente");
           }else{
            Console.WriteLine("No se encontró un videojuego con ese nombre");
           }
        }catch(Exception e ){
            _repository.LogError("Error deleting videogame", e);
            throw new Exception("An error ocurred deleting the videogame", e);
        }
    }

    public void UpdateVideogame(string videogameName,  VideogameUpdateDTO videogameUpdateDTO){
        var videogame = _repository.GetVideogame(videogameName);
       if (videogame==null){
         throw new KeyNotFoundException($"Videojuego con nombre {videogameName} no encontrado");
       }

       videogame.Name= videogameUpdateDTO.Name;
       videogame.Genre=videogameUpdateDTO.Genre;
       videogame.Description=videogameUpdateDTO.Description;
       videogame.Price=videogameUpdateDTO.Price;
       videogame.Developer=videogameUpdateDTO.Developer;
       videogame.Platform=videogameUpdateDTO.Platform;
       videogame.Valoration=videogameUpdateDTO.Valoration;
       _repository.UpdateVideogame(videogame);
       _repository.SaveChanges();
    }
     public string InputEmpty()
    {
        try
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("El campo está vacío.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el campo", e);
            throw new Exception("Ha ocurrido un error al comprobar el campo", e);
        }
    }

 


}
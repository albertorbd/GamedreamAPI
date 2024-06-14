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
    
    throw new Exception("An error ocurred registering the Videogame", e);
 }
}

public  IEnumerable<Videogame> GetAllVideogames(VideogameQueryParameters videogameQueryParameters, bool orderByIdDesc){
   
  return _repository.GetAllVideogames(videogameQueryParameters, orderByIdDesc);
}

    
    public Videogame GetVideogameByName(string videogameName){
        try{
            return _repository.GetVideogameByName(videogameName);
        }
        catch(Exception e){
            
            throw new Exception("An error has ocurred getting the videogame", e);
        }
    }

    public Videogame GetVideogameById(int videogameId){
        try{
            return _repository.GetVideogameById(videogameId);
        }
        catch(Exception e){
            
            throw new Exception("An error has ocurred getting the videogame", e);
        }
    }

    public void DeleteVideogame(int videogameId){
         
        try{
           Videogame getVideogame = GetVideogameById(videogameId);

           if (getVideogame != null){
           _repository.DeleteVideogame(getVideogame);
           _repository.SaveChanges();
           Console.WriteLine("El videojuego ha sido borrado correctamente");
           }else{
            Console.WriteLine("No se encontró un videojuego con ese nombre");
           }
        }catch(Exception e ){
            
            throw new Exception("An error ocurred deleting the videogame", e);
        }
    }

    public void UpdateVideogame(int videogameId,  VideogameUpdateDTO videogameUpdateDTO){
        var videogame = _repository.GetVideogameById(videogameId);
       if (videogame==null){
         throw new KeyNotFoundException($"Videogame with ID {videogameId} wasnt found");
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
            throw new Exception("An error occurred while checking the field", e);
        }
    }

 


}
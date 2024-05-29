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


    public void RegisterVideogame(string name, string genre, string description, double price, string developer, string platform, int valoration){
 try
 {
    Videogame videogame= new(name,genre, description, price, developer, platform, valoration);
    _repository.AddVideogame(videogame);
    _repository.SaveChanges();
 }   
 catch(Exception e){
    _repository.LogError("Error registering the videogame", e);
    throw new Exception("An error ocurred registering the Videogame", e);
 }
}

public void PrintAllVideogames(){
    try
    {
        Dictionary<string, Videogame> videogames= _repository.GetAllVideogames();
        Console.WriteLine("Lista de videojuegos: \n");
        foreach (var videogame in videogames.Values){
            Console.WriteLine($"ID: {videogame.Id}, Nombre: {videogame.Name}, Género: {videogame.Genre}, Descripción: {videogame.Description}, Desarrollador: {videogame.Developer}, Plataforma: {videogame.Platform}, Valoración:{videogame.Valoration}, Fecha de registro: {videogame.RegisterDate} ");
        }
    }catch(Exception e )
    {
        _repository.LogError("Error printing the videogames", e);
        throw new Exception("An error has ocurred printing the videogames", e);
    }
}

    public bool CheckVideogame(string name)
    {
        try
        {
            var videogames = _repository.GetAllVideogames();
            foreach (var videogame in videogames.Values)
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

    public void UpdateVideogame(Videogame updatedVideogame, string newGenre, string newDescription, string newDeveloper, string newPlatform, int newValoration){
        try{
        updatedVideogame.Genre= newGenre;
        updatedVideogame.Description= newDescription;
        updatedVideogame.Developer= newDeveloper;
        updatedVideogame.Platform=newPlatform;
        updatedVideogame.Valoration=newValoration;
        _repository.UpdateVideogame(updatedVideogame);
        _repository.SaveChanges();
        }catch(Exception e){
            _repository.LogError("Error updating videogame",e);
            throw new Exception("An error has ocurred updating videogame");

        }
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

 public void SearchVideogameByName()
{
    try{
    string searchQuery;
    do
    {
        Console.WriteLine("Escribe S para salir.");
        Console.WriteLine("Buscar videojuego por nombre (mayúsculas y minúsculas): ");
        searchQuery = InputEmpty();

        if (searchQuery.ToLower() == "s")
        {
            break;
        }

        var matchingVideogames = _repository.GetAllVideogames().Values
            .Where(v => v.Name.ToLower().Contains(searchQuery.ToLower()));

        if (!matchingVideogames.Any())
        {
            Console.WriteLine("No se ha encontrado ningún videojuego con ese nombre.\n");
        }
        else
        {
            Console.WriteLine("Resultados de la búsqueda:\n");
            foreach (var videogame in matchingVideogames)
            {
                Console.WriteLine($"ID: {videogame.Id}, Nombre: {videogame.Name}, Género: {videogame.Genre}, Descripción: {videogame.Description}, Precio: {videogame.Price},Desarrollador: {videogame.Developer}, Plataforma: {videogame.Platform}, Valoración: {videogame.Valoration}, Fecha de Registro: {videogame.RegisterDate}");
            }
            Console.WriteLine();
        }
    } while (searchQuery.ToLower() != "s");
    } catch (Exception e)
        {
            _repository.LogError("Error searching videogame", e);
            throw new Exception("An eror has ocurred searching the videogame", e);
        }

    }



}
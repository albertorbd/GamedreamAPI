using Gamedream.Business;
using Gamedream.Models;

namespace Gamedream.Presentation;

public class publicMenu
{
    public readonly IUserService _userService;
    public readonly IVideogameService _VideogameService;
    private User currentUser;

    public publicMenu(IUserService userService, IVideogameService videogameService)
    {
        _userService = userService;
        _VideogameService = videogameService;
    }
    
    public void MainPublicMenu(string userEmail)
    {
        currentUser = _userService.GetUser(userEmail);

        Console.WriteLine("\n----------BIENVENIDO A GAMEDREAM----------\n");
        Console.WriteLine($"Bienvenido {currentUser.Name} {currentUser.Lastname} !\n");
        Console.WriteLine($" Efectivo: {currentUser.Money} €\n");
        Console.WriteLine("1. Mi cuenta");
        Console.WriteLine("2. Buscar Videojuego");
        Console.WriteLine("3. Cerrar sesión");
        PublicMenuOptions(Console.ReadLine());
    }

    public void PublicMenuOptions(string option)
    {
        Menu menu = new(_userService, _VideogameService);
        privateMenu privateMenu = new(_userService, _VideogameService);

        switch (option)
        {
        case "1":
            privateMenu.MainPrivateMenu(currentUser.Email);
        break;
        case "2":
            _VideogameService.PrintAllVideogames();
            _VideogameService.SearchVideogameByName();
            MainPublicMenu(currentUser.Email);
        break;
        case "3":
            Console.WriteLine("Se ha cerrado la sesión");
            menu.Registration();
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            PublicMenuOptions(Console.ReadLine());
        break;
        }
    }

}
    
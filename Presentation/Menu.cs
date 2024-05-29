using System.Collections;
using System.Runtime.CompilerServices;
using Gamedream.Business;
using Gamedream.Models;


using System;
using System.ComponentModel.Design;

namespace Gamedream.Presentation
{
    public class Menu
    {
        private readonly IVideogameService _videogameService;
        public readonly IUserService _userService;

        public Menu(IUserService userService, IVideogameService videogameService)
        {
         _userService = userService;

         _videogameService = videogameService;
        }

        public void Registration()
        {
            Console.WriteLine("\n-----------BIENVENIDO A GAMEDREAM-------------\n");
            Console.WriteLine("1. Regístrate");
            Console.WriteLine("2. Inicia sesión");
            Console.WriteLine("3. Admin mode");
            Console.WriteLine("4. Salir");
            SelectRegistrationOption(Console.ReadLine()); 
        }

         public void SelectRegistrationOption(string option)
    {
        switch (option)
        {
        case "1":
            SignUp();
        break;
        case "2":
            Login();
        break;
        case "3":
         goToAdminMode();
        break;

        case "4":
        Console.WriteLine("Vuelve cuando quieras");
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            Registration();
        break;
        }
    }

    private void SignUp() 
    {
        Console.Write("Nombre: ");
        string name = _userService.InputEmpty();
         Console.Write("Apellidos: ");

        string lastname= _userService.InputEmpty();
        
      
        Console.Write("Dirección de correo: ");
        string email = _userService.InputEmpty();

        Console.Write("Contraseña: ");
        string password = _userService.InputEmpty();

       

        Console.Write("DNI: ");
        string dni = _userService.InputEmpty();

         Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
        DateTime birthday = CheckDate();

        
        if (_userService.CheckRepeatUser(dni, email))
        {
            Console.WriteLine("Ya existe una cuenta asociada a uno de estos datos, pruebe otra vez.");
            Registration();
        }
        else
        {
            if (email.Contains("@"))
            {
                _userService.RegisterUser(name,lastname , email, password,  dni, birthday);
                Console.WriteLine("¡Registro completado!");
                GoToPublicMenu(email);
            }
            else
            {
                Console.WriteLine("El correo debe de contener @.");
                Registration();
            }
        }
    }

    private DateTime CheckDate()
{
    DateTime birthday;
    string input;
    
    do
    {
        input = Console.ReadLine();
        
        if (DateTime.TryParse(input, out birthday))
        {
            return birthday;
        } 
        else 
        {
            Console.WriteLine("La fecha introducida es incorrecta. Inténtelo de nuevo.");
        }

    } while (true);
}


    private void GoToPublicMenu(string email)
    {
        publicMenu publicUserMenu = new(_userService, _videogameService);
        publicUserMenu.MainPublicMenu(email);
    }

     private void Login() {
        Console.Write("Email: ");
        string email = _userService.InputEmpty();
        Console.Write("Contraseña: ");
        string password = _userService.InputEmpty();

        if (_userService.loginCheck(email, password))
        {
            GoToPublicMenu(email);
        } 
       
        else
        {
            Console.WriteLine("El correo o la contraseña introducida es incorrecta.");
            Registration();
        }
    }

    private void goToAdminMode(){
         Console.WriteLine("Has iniciado sesión como Admin");
            AdminMenu adminMenu = new(_userService, _videogameService);
            adminMenu.MainAdminMenu();
    }
    }

    


    }

    


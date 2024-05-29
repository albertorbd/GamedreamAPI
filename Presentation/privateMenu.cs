using Gamedream.Business;
using Gamedream.Models;

namespace Gamedream.Presentation;

public class privateMenu
{
    public readonly IUserService _userService;
    public readonly IVideogameService _videogameService;
    private User currentUser;

    public privateMenu(IUserService userService, IVideogameService videogameService)
    {
        _userService = userService;
        _videogameService = videogameService;
    }

    public void MainPrivateMenu(string userEmail)
    {
        currentUser = _userService.GetUser(userEmail);

        Console.WriteLine("\n ----------Gamedream------------------\n");
        Console.WriteLine($" Efectivo: {currentUser.Money} €");
        Console.WriteLine($"Nombre: {currentUser.Name}");
        Console.WriteLine($"Apellidos: {currentUser.Lastname}");
        Console.WriteLine($"Correo: {currentUser.Email}");
        Console.WriteLine($"Fecha de nacimiento: {currentUser.BirthDate}");
        Console.WriteLine($"DNI: {currentUser.DNI}");

        Console.WriteLine("1. Modificar correo | 2. Modificar contraseña");
        Console.WriteLine("3. Depositar dinero");
        Console.WriteLine("4. Retirar dinero");
        Console.WriteLine("5. Comprar videojuego");
        Console.WriteLine("6. Borrar cuenta");
        Console.WriteLine("7. Lista de Videojuegos");
        Console.WriteLine("8. Lista de Operaciones");
        Console.WriteLine("9. Cerrar sesión");


        SelectPrivateUserMenuOption(Console.ReadLine());

        
    }
     public void SelectPrivateUserMenuOption(string option)
    {
        Menu menu = new(_userService, _videogameService);
        publicMenu publicMenu = new(_userService, _videogameService);

        switch (option)
        {
        
        case "1":
            Console.Write("Nuevo email: ");
            string email = _userService.InputEmpty();
            UpdateEmail(email);
            MainPrivateMenu(currentUser.Email);
        break;
        case "2":
            Console.Write("Nueva contraseña: ");
            string password = _userService.InputEmpty();
            UpdatePassword(password);
            MainPrivateMenu(currentUser.Email);
        break;
        case "3":
            MakeDeposit();
            MainPrivateMenu(currentUser.Email);
        break;
        case "4":
            MakeWithdrawal();
            MainPrivateMenu(currentUser.Email);
        break;
          case "5":
          BuyVideogame();
          MainPrivateMenu(currentUser.Email);
        break;

        case "6":
        _userService.DeleteUser(currentUser.Email);
        Console.WriteLine("Tu cuenta ha sido eliminada correctamente");
        menu.Registration();
            
        break;

        case "7":
        _userService.PrintVideogameBought(currentUser);
        MainPrivateMenu(currentUser.Email);

        break;

        case "8":
        _userService.PrintOperations(currentUser);
        MainPrivateMenu(currentUser.Email);
            
        break;
        case "9":
       menu.Registration();
            
        break;
       
       
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectPrivateUserMenuOption(Console.ReadLine());
        break;
        }
    }
    
    private void MakeDeposit()
{
    Console.Write("Añadir dinero: ");
    string amountInput = _userService.InputEmpty();

    Console.WriteLine("Seleccione el método de pago:");
    Console.WriteLine("1. Tarjeta de crédito");
    Console.WriteLine("2. PayPal");
    Console.WriteLine("3. Visa");
    Console.Write("Introduce tu método de pago: ");

    string paymentMethodOption = _userService.InputEmpty();
    string paymentMethod = "";

    switch (paymentMethodOption)
    {
        case "1":
            paymentMethod = "Tarjeta de crédito";
            break;
        case "2":
            paymentMethod = "PayPal";
            break;
        case "3":
            paymentMethod = "Visa";
            break;
        default:
            Console.WriteLine("Introduce una opción válida");
            MakeDeposit();
            return;
    }

    _userService.Deposit(currentUser, "Ingreso", amountInput, paymentMethod);
    MainPrivateMenu(currentUser.Email);
}

 private void MakeWithdrawal()
{
    Console.Write("Retirar dinero ");
    string amountInput = _userService.InputEmpty();

    Console.WriteLine("Seleccione dónde quieres retirar el dinero:");
    Console.WriteLine("1. Tarjeta de crédito");
    Console.WriteLine("2. PayPal");
    Console.WriteLine("3. Visa");
    Console.Write("Introduce tu método de pago: ");

    string withDrawMethodOption = _userService.InputEmpty();
    string withdrawMethod = "";

    switch (withDrawMethodOption)
    {
        case "1":
            withdrawMethod = "Tarjeta de crédito";
            break;
        case "2":
            withdrawMethod= "PayPal";
            break;
        case "3":
            withdrawMethod = "Visa";
            break;
        default:
            Console.WriteLine("Introduce una opción válida");
            MakeWithdrawal();
            return;
    }

    _userService.Withdrawal(currentUser, "Retirada", amountInput, withdrawMethod);
    MainPrivateMenu(currentUser.Email);
}

   private void UpdateEmail(string newEmail){
   _userService.UpdateUser(currentUser.Email, newEmail:newEmail);
   Console.WriteLine("Tu correo ha sido actualizado correctamente");
   }

   private void UpdatePassword(string newPassword){
   _userService.UpdateUser(currentUser.Email, newPassword:newPassword);
   Console.WriteLine("Contraseña actualizada");
   }

    private void BuyVideogame(){
     _videogameService.PrintAllVideogames();
    Console.Write("Nombre del videojuego que quieres comprar: ");
    string videoGameName = Console.ReadLine();
    if(_videogameService.CheckVideogame(videoGameName)){
     Videogame videoGame = _videogameService.GetVideogame(videoGameName);
    Console.WriteLine($"Precio del videojuego {videoGame.Name}: {videoGame.Price}€");
    Console.Write("¿Deseas comprar este videojuego? (S/N): ");
     string choice = Console.ReadLine().ToUpper();

        if (choice == "S")
        {
            try
            {
                _userService.BuyVideogame(currentUser, videoGame, $"Compra del videojuego {videoGame.Name}");
                Console.WriteLine($"Has comprado el videojuego {videoGame.Name}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }
    else
    {
        Console.WriteLine("No se ha encontrado ningún videojuego por ese nombre.");
    }
    
    
    }
    }
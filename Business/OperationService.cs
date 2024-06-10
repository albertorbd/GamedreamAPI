
using GamedreamAPI.Data;
using GamedreamAPI.Models;


namespace GamedreamAPI.Business;
public class OperationService : IOperationService
{

private readonly IOperationRepository _operationrepository;
private readonly IUserRepository _userRepository;
private readonly IVideogameRepository _videogameRepository;


    public OperationService(IOperationRepository operationrepository, IUserRepository userRepository, IVideogameRepository videogameRepository )
    {
        _operationrepository = operationrepository;
        _userRepository = userRepository;
        _videogameRepository= videogameRepository;
    }

public void MakeDeposit(MoneyTransferDTO moneyTransferDTO)
    {
        var user = _userRepository.GetUserById(moneyTransferDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {moneyTransferDTO.UserId} no encontrado");
        }

        Operation operation = new("Ingresar dinero", moneyTransferDTO.Amount, moneyTransferDTO.Method, moneyTransferDTO.UserId);
        //user.Transactions.Add(transaction);
        user.Money += moneyTransferDTO.Amount;
        _userRepository.UpdateUser(user);
        _operationrepository.AddOperation(operation);
    }
    public void MakeWithdrawal(MoneyTransferDTO moneyTransferDTO)
    {
        var user = _userRepository.GetUserById(moneyTransferDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {moneyTransferDTO.UserId} no encontrado");
        }

         if (user.Money < moneyTransferDTO.Amount)
    {
        throw new InvalidOperationException("Fondos insuficientes para realizar esta operación");
    }

        Operation operation = new("Retirar dinero", moneyTransferDTO.Amount, moneyTransferDTO.Method, moneyTransferDTO.UserId);
        //user.Transactions.Add(transaction);
        user.Money -= moneyTransferDTO.Amount;
        _userRepository.UpdateUser(user);
        _operationrepository.AddOperation(operation);
    }

    public void BuyVideogame(BuyVideogameDTO buyVideogameDTO)
{
    var user = _userRepository.GetUserById(buyVideogameDTO.UserId);
    var videogame = _videogameRepository.GetVideogameById(buyVideogameDTO.VideogameId);
    if (user == null) 
        throw new KeyNotFoundException($"Usuario con ID {buyVideogameDTO.UserId} no encontrado");
    if (videogame == null) 
        throw new KeyNotFoundException($"Videojuego con ID {buyVideogameDTO.VideogameId} no encontrado");
        
    if (user.Money < videogame.Price)
        throw new Exception("No tienes suficiente saldo para realizar la compra");

    double price = videogame.Price ?? 0.0;

    // Crear la operación utilizando el constructor correcto
    Operation operation = new(buyVideogameDTO.VideogameId, $"Comprar {videogame.Name} con precio {videogame.Price}", price, buyVideogameDTO.UserId, 1);
    
    user.Money -= price;
    _userRepository.UpdateUser(user);
    _operationrepository.AddOperation(operation);
}

public IEnumerable<Operation> GetAllOperations(int userId, OperationQueryParameters operationQueryParameters)
    {
        return _operationrepository.GetAllOperations(userId, operationQueryParameters);
    }


  public Dictionary<string, double> VideogamesPurchased(int userId)
{
    var userOperations = _operationrepository.GetAllOperations(userId, new OperationQueryParameters());

    var totalQuantityByVideogame = new Dictionary<string, double>();

    foreach (var operation in userOperations)
    {
        if (operation.VideogameId.HasValue)
        {
            var videogame = _videogameRepository.GetVideogameById(operation.VideogameId.Value);
            var videogameName = videogame.Name;

            if (!totalQuantityByVideogame.TryGetValue(videogameName, out var currentQuantity))
            {
                currentQuantity = 0;
            }

            totalQuantityByVideogame[videogameName] = currentQuantity + operation.Quantity;
        }
    }

    return totalQuantityByVideogame;
}
}


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
            throw new KeyNotFoundException($"User with ID {moneyTransferDTO.UserId} wasnt found");
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
            throw new KeyNotFoundException($"User with ID {moneyTransferDTO.UserId} wasnt found");
        }

         if (user.Money < moneyTransferDTO.Amount)
    {
        throw new InvalidOperationException("Insufficient founds to make this operation");
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
        throw new KeyNotFoundException($"User witn ID {buyVideogameDTO.UserId} wasnt found");
    if (videogame == null) 
        throw new KeyNotFoundException($"Videogame with ID {buyVideogameDTO.VideogameId} wasnt found");
    
    var userOperations = _operationrepository.GetAllOperations(buyVideogameDTO.UserId, new OperationQueryParameters());
    if (userOperations.Any(op => op.VideogameId == buyVideogameDTO.VideogameId))
        throw new Exception("You already have that videogame");
    
    
    if (user.Money < videogame.Price)
        throw new Exception("You do not have enough balance to make the purchase");

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


 public List<string> VideogamesPurchased(int userId)
{
    var userOperations = _operationrepository.GetAllOperations(userId, new OperationQueryParameters());

    var videogamesPurchased = new List<string>();

    foreach (var operation in userOperations)
    {
        if (operation.VideogameId.HasValue)
        {
            var videogame = _videogameRepository.GetVideogameById(operation.VideogameId.Value);
            var videogameName = videogame.Name;

            if (!videogamesPurchased.Contains(videogameName))
            {
                videogamesPurchased.Add(videogameName);
            }
        }
    }

    return videogamesPurchased;
}
}

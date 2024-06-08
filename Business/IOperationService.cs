using System.Reflection.PortableExecutable;
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IOperationService
{
void MakeDeposit(MoneyTransferDTO moneyTransferDTO);
void MakeWithdrawal(MoneyTransferDTO moneyTransferDTO);
void BuyVideogame(BuyVideogameDTO buyVideogameDTO);

public IEnumerable<Operation> GetAllOperations(OperationQueryParameters operationQueryParameters);

public Dictionary<string, double> VideogamesPurchased(int userId); 
}
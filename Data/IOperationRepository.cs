using GamedreamAPI.Models;

namespace GamedreamAPI.Data;

public interface IOperationRepository{
void AddOperation(Operation operation); 
void SaveChanges();
IEnumerable<Operation> GetAllOperations(int userId, OperationQueryParameters operationQueryParameters);

}
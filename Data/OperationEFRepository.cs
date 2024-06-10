using GamedreamAPI.Models;


namespace GamedreamAPI.Data
{
    public class OperationEFRepository : IOperationRepository
    {
        private readonly GamedreamContext _context;

        public OperationEFRepository(GamedreamContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddOperation(Operation operation)
        {
            _context.Operations.Add(operation);
            SaveChanges();
        }

        public IEnumerable<Operation> GetAllOperations(int userId, OperationQueryParameters operationQueryParameters) 
        {   
            var query = _context.Operations.AsQueryable();
            
            query = _context.Operations.Where(t => t.UserId == userId);

            if (operationQueryParameters.VideogameId.HasValue)
            {
                query = query.Where(t => t.VideogameId == operationQueryParameters.VideogameId);
            }

          

            if (!string.IsNullOrWhiteSpace(operationQueryParameters.Concept))
            {
                query = query.Where(t => t.Concept.StartsWith(operationQueryParameters.Concept));
            }
            
            query = query.OrderByDescending(t => t.Date);

            var result = query.ToList();
            return result;
        }


        

    }   
}
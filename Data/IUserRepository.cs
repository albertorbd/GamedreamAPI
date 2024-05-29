using GamedreamAPI.Models;

namespace GamedreamAPI.Data;

public interface IUserRepository{
void AddUser(User user); 
 IEnumerable<User> GetAllUsers();
 User GetUser(string email);
 void DeleteUser(User user);
 void UpdateUser(User user);
 void SaveChanges();
 void LogError(string message, Exception exception);

}
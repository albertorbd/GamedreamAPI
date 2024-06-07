using GamedreamAPI.Models;

namespace GamedreamAPI.Data;

public interface IUserRepository{
void AddUser(User user); 
IEnumerable<User> GetAllUsers();
User GetUserByEmail(string email);
User GetUserById(int UserId);
void DeleteUser(User user);
void UpdateUser(User user);
void SaveChanges();


}
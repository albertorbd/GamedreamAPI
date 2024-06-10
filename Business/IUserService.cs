
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IUserService
{
User RegisterUser(UserCreateDTO userCreateDTO);
IEnumerable<User> GetAllUsers();  
bool CheckRepeatUser(string email, string dni);
User GetUserByEmail(string email);
User GetUserById(int userId);
void DeleteUser(int userId);
void UpdateUser(int userId, UserUpdateDTO userUpdateDTO);

User loginCheck(string email, string password);
bool IsEmailTaken(string email);
}
using System.Reflection.PortableExecutable;
using GamedreamAPI.Models;
namespace GamedreamAPI.Business;

public interface IUserService
{
void RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate);
IEnumerable<User> GetAllUsers();  
bool CheckRepeatUser(string email, string dni);
User GetUser(string email);
void DeleteUser(string email);
void UpdateUser(string email, string newEmail=null, string newPassword= null);

bool loginCheck(string email, string password);
bool IsEmailTaken(string email);
}
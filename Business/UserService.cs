using System.Security.Cryptography;
using Microsoft.VisualBasic;

using GamedreamAPI.Data;
using GamedreamAPI.Models;
using System.Buffers;
using System.Reflection.Metadata.Ecma335;

namespace GamedreamAPI.Business;
public class UserService : IUserService
{

private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

public User RegisterUser(UserCreateDTO userCreateDTO){
try
 {
    User user= new(userCreateDTO.Name, userCreateDTO.Lastname, userCreateDTO.Email, userCreateDTO.Password, userCreateDTO.DNI, userCreateDTO.BirthDate);
    _repository.AddUser(user);
    return user;
 }   
 catch(Exception e){
    
    throw new Exception("An error ocurred registering the user", e);
 }    
}

public  IEnumerable<User> GetAllUsers(){
   
  return _repository.GetAllUsers();
}

public bool CheckRepeatUser(string dni, string email){
try
    {
    foreach (var user in _repository.GetAllUsers())
     {
        if (user.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase) || 
        user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
         {
          return true;
         }
        }

            return false;
        }
        catch (Exception e)
        {
            
            throw new Exception("An error has ocurred checking user", e);
        }
}

public User GetUserByEmail(string email){
        try{
            return _repository.GetUserByEmail(email);
        }
        catch(Exception e){
           
            throw new Exception("An error has ocurred getting the user", e);
        }
    }

 public User GetUserById(int idUser){
        try{
            return _repository.GetUserById(idUser);
        }
        catch(Exception e){
           
            throw new Exception("An error has ocurred getting the user", e);
        }
    }   

public void DeleteUser(int userId){
         
        try{
          User getUser = GetUserById(userId);

           if (getUser != null){
           _repository.DeleteUser(getUser);
           _repository.SaveChanges();
           Console.WriteLine("User has been removed");
           }else{
            Console.WriteLine("No user found");
           }
        }catch(Exception e ){
            
            throw new Exception("An error ocurred deleting the user", e);
        }
    }


  public void UpdateUser(int userId,  UserUpdateDTO userUpdateDTO){
    
       var user = _repository.GetUserById(userId);
       if (user==null){
         throw new KeyNotFoundException($"Usuario con Id {userId} no encontrado");
       }

       user.Email= userUpdateDTO.Email;
       user.Password=userUpdateDTO.Password;
       _repository.UpdateUser(user);
       _repository.SaveChanges();
    }  

public bool IsEmailTaken(string email){
        try{
            var users= _repository.GetAllUsers();
            foreach(var user in users){
                if(user.Email==email){
                    return true;
                }
            }
            return false;
        }catch (Exception e)
        {
            
            throw new Exception("An error has ocurred checking if email is in use", e);
        }

        }
   

    
    
public User loginCheck(string email, string password)
{
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        throw new ArgumentException("El email y la contraseña son obligatorios.");
    }

    foreach (var userToLog in _repository.GetAllUsers())
    {
        if (userToLog.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            userToLog.Password.Equals(password))
        {
            return userToLog;
        }
    }
    return null;
}

}

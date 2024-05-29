using System.Security.Cryptography;
using Microsoft.VisualBasic;

using GamedreamAPI.Data;
using GamedreamAPI.Models;
using System.Buffers;

namespace GamedreamAPI.Business;
public class UserService : IUserService
{

private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

public void RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate){
try
 {
    User user= new(name,lastname, email, password, dni, birthdate);
    _repository.AddUser(user);
    _repository.SaveChanges();
 }   
 catch(Exception e){
    _repository.LogError("Error registering the user", e);
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
            _repository.LogError("Error checking user", e);
            throw new Exception("An error has ocurred checking user", e);
        }
}

public User GetUser(string email){
        try{
            return _repository.GetUser(email);
        }
        catch(Exception e){
            _repository.LogError("Error getting the user", e);
            throw new Exception("An error has ocurred getting the user", e);
        }
    }

public void DeleteUser(string userEmail){
         
        try{
          User getUser = GetUser(userEmail);

           if (getUser != null){
           _repository.DeleteUser(getUser);
           _repository.SaveChanges();
           Console.WriteLine("User has been removed");
           }else{
            Console.WriteLine("No user found");
           }
        }catch(Exception e ){
            _repository.LogError("Error deleting user", e);
            throw new Exception("An error ocurred deleting the user", e);
        }
    }


  public void UpdateUser(string userEmail, string  newEmail= null, string newPassword=null){
    
        try{
        User userUpdated= _repository.GetUser(userEmail);

        if(!string.IsNullOrEmpty(newEmail) && IsEmailTaken(newEmail)){
            Console.WriteLine("El correo está siendo utilizado por otro usuario");
            return;
        }
         if (!string.IsNullOrEmpty(newEmail))
            {
                userUpdated.Email = newEmail;
                
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                userUpdated.Password = newPassword;
                
            }
        
        
        _repository.UpdateUser(userUpdated);
        _repository.SaveChanges();
        

        }catch(Exception e){
            _repository.LogError("Error updating user",e);
            throw new Exception("An error has ocurred updating user");

        }
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
            _repository.LogError("Error checking email", e);
            throw new Exception("An error has ocurred checking if email is in use", e);
        }

        }
   

    
    
public bool loginCheck(string email, string password){
try{
   foreach(var user in _repository.GetAllUsers()){
    if((user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
     user.Password.Equals(password))){
        return true;
     }
   } 
   return false;
}
catch (Exception e)
        {
            _repository.LogError("error checking user", e);
            throw new Exception("An error has ocurred checking user", e);
        }
}

}

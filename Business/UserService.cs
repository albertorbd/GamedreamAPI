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

public User RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate){
try
 {
    User user= new(name,lastname, email, password, dni, birthdate);
    _repository.AddUser(user);
    _repository.SaveChanges();
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

public User GetUser(string email){
        try{
            return _repository.GetUser(email);
        }
        catch(Exception e){
           
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
            
            throw new Exception("An error ocurred deleting the user", e);
        }
    }


  public void UpdateUser(string userEmail,  UserUpdateDTO userUpdateDTO){
    
       var user = _repository.GetUser(userEmail);
       if (user==null){
         throw new KeyNotFoundException($"Usuario con email {userEmail} no encontrado");
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
            
            throw new Exception("An error has ocurred checking user", e);
        }
}

}

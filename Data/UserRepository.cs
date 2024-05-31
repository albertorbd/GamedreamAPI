using GamedreamAPI.Models;
using System.Text.Json;

namespace GamedreamAPI.Data;
public class UserRepository : IUserRepository
{

 private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly string _filePath =  "../Presentation/users.json";
    private readonly string _logsFilePath = "../Presentation/logs.json";
    public UserRepository()
    {
        LoadUsers();
    }
    
    public void AddUser(User user)
    {
         
        _users[user.Id.ToString()] = user;
    }

    public IEnumerable<User> GetAllUsers(){
         return _users.Values;
     }

     public User GetUser(string emailUser){
        var allUsers = GetAllUsers();
            foreach (var user in allUsers)
            {
                if (user.Email.Equals(emailUser, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            
            return null;
     }

    public void DeleteUser(User user){
    _users.Remove(user.Id.ToString());
    }

     public void UpdateUser(User user){
         
         _users[user.Id.ToString()]= user;
            
        
    }

     public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            LogError("Error saving changes", e);
            throw new Exception("Error saving changes in users archive", e);
        }
    }

    public void LoadUsers()
    {
     if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(jsonString);
                _users = users.ToDictionary(user => user.Id.ToString());
                int highestId = _users.Values.Max(v => v.Id);

            
                 User.UserIdSeed = highestId + 1;


                
            }
            catch (Exception e)
            {
                LogError("Error searching users archive", e);
                 throw new Exception("Error searching users archive", e);
            }
        }
        

    }

    public void LogError(string message, Exception exception)
    {
        try
        {
            using (StreamWriter writer= new StreamWriter(_logsFilePath, true)){
                writer.WriteLine($"{DateTime.Now} ERROR {message}");
                writer.WriteLine(exception.ToString());
                writer.WriteLine();
            }
            
        }
        catch (Exception e)
        {
            throw new Exception("Error has ocurred writting in logs", e);
        }
    }

}
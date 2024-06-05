using GamedreamAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamedreamAPI.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly GamedreamContext _context;

        public UserEFRepository(GamedreamContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        public IEnumerable<User> GetAllUsers() 
        {
            var users = _context.Users;
            return users;
        }

        public User GetUser(string userEmail)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == userEmail);
            return user;
        }


        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteUser(User userDelete) {
            var user = GetUser(userDelete.Email);
            _context.Users.Remove(user);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        

    }   
}
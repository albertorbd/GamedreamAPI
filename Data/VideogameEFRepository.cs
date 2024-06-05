using GamedreamAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamedreamAPI.Data
{
    public class VideogameEFRepository : IVideogameRepository
    {
        private readonly GamedreamContext _context;

        public VideogameEFRepository(GamedreamContext context)
        {
            _context = context;
        }

        public void AddVideogame(Videogame videogame)
        {
            _context.Videogames.Add(videogame);
            SaveChanges();
        }

        public IEnumerable<Videogame> GetAllVideogames() 
        {
            var query = _context.Videogames.AsQueryable();

           
            var result = query.ToList();
            return result;
        }

        public Videogame GetVideogame(string name)
        {
            var videogame = _context.Videogames.FirstOrDefault(videogame => videogame.Name == name);
            return videogame;
        }

        public void UpdateVideogame(Videogame videogame)
        {
            _context.Entry(videogame).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteVideogame(Videogame videogameToDelete) {
            var videogame = GetVideogame(videogameToDelete.Name);
            _context.Videogames.Remove(videogame);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
 
    }   
}
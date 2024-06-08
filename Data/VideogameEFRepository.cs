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

        public IEnumerable<Videogame> GetAllVideogames(VideogameQueryParameters videogameQueryParameters,  bool orderByIdDesc) 
        {
            var query = _context.Videogames.AsQueryable();

           
            if (!string.IsNullOrWhiteSpace(videogameQueryParameters.Name))
            {
                query = query.Where(c => c.Name.Contains(videogameQueryParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(videogameQueryParameters.Genre))
            {
                query = query.Where(c => c.Genre.Contains(videogameQueryParameters.Genre));
            }

            if (!string.IsNullOrWhiteSpace(videogameQueryParameters.Developer))
            {
                query = query.Where(c => c.Developer.StartsWith(videogameQueryParameters.Developer));
            }

             if (!string.IsNullOrWhiteSpace(videogameQueryParameters.Platform))
            {
                query = query.Where(c => c.Platform.StartsWith(videogameQueryParameters.Platform));
            }

            if (orderByIdDesc) 
    {
        query = query.OrderByDescending(p => p.Id);
    }

            var result = query.ToList();
            return result;
        }

         
        public Videogame GetVideogameByName(string name)
        {
            var videogame = _context.Videogames.FirstOrDefault(videogame => videogame.Name == name);
            return videogame;
        }

         public Videogame GetVideogameById(int id)
        {
            var videogame = _context.Videogames.FirstOrDefault(videogame => videogame.Id == id);
            return videogame;
        }

        public void UpdateVideogame(Videogame videogame)
        {
            _context.Entry(videogame).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteVideogame(Videogame videogameToDelete) {
            var videogame = GetVideogameByName(videogameToDelete.Name);
            _context.Videogames.Remove(videogame);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
 
    }   
}
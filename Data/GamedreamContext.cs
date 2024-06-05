using Microsoft.EntityFrameworkCore;
using GamedreamAPI.Models;
using Microsoft.Extensions.Logging;

namespace GamedreamAPI.Data
{
    public class GamedreamContext : DbContext
    {

        public GamedreamContext(DbContextOptions<GamedreamContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Jesus", Lastname = "Lopez", Email = "jesusito@hotmail.es", Password = "12345", DNI = "345678", BirthDate = new DateTime(1990, 12, 20), Money = 60},

            new User { Id = 2, Name = "Alberto", Lastname = "Riveiro del Bano", Email = "albertoriveiro@hotmail.es", Password = "12345", DNI = "2532135", BirthDate = new DateTime(1996, 10, 2), Money = 50},

            new User { Id = 3, Name = "carlos", Lastname = "diaz", Email = "carlitos@hotmail.es", Password = "carlitos12", DNI = "234567", BirthDate = new DateTime(1980, 2, 10), Money = 0},

            new User { Id = 4, Name = "Marcos", Lastname = "M", Email = "marcos@hotmail.es", Password = "caballo14", DNI = "2312313123", BirthDate = new DateTime(1995, 5, 31, 12, 41, 4), Money = 0 },

            new User { Id = 5, Name = "Mario", Lastname = "Bes", Email = "mariobes@hotmail.com", Password = "caballo1213123", DNI = "21312321313", BirthDate = new DateTime(2002, 5, 31, 12, 49, 44), Money = 0 });


            modelBuilder.Entity<Videogame>().HasData(
                new Videogame { Id = 1, Name = "Dragons dogma 2", Genre = "Aventura y Rol", Description = "Eres un Arisen que tiene que recuperar su corazón", RegisterDate = new DateTime(2024, 5, 3, 13, 18, 45), Price = 70, Developer = "Capcom", Platform = "PC PS5 Y XBOX", Valoration = 10 },

                new Videogame { Id = 2, Name = "Persona 5", Genre = "Rol", Description = "Juego de rol muy famoso", RegisterDate = new DateTime(2024, 5, 3, 13, 28, 28), Price = 60, Developer = "Balve", Platform = "PC y PS4", Valoration = 10 },

                new Videogame { Id = 3, Name = "Final Fantasy XV", Genre = "Aventuras y Rol", Description = "Subir niveles y matar monstruos", RegisterDate = new DateTime(2024, 5, 5, 23, 54, 31), Price = 50, Developer = "Square Enix", Platform = "PS5", Valoration = 9 },

                new Videogame { Id = 4, Name = "Manor lords", Genre = "Estrategia", Description = "Construye tu imperio", RegisterDate = new DateTime(2024, 5, 6, 0, 8, 2), Price = 40, Developer = "Three Kingdom", Platform = "PC", Valoration = 9 },

                new Videogame { Id = 5, Name = "Max Payne 4", Genre = "Acción", Description = "Juego de acción con buena historia", RegisterDate = new DateTime(2024, 6, 1, 2, 29, 36), Price = 40, Developer = "ASASSA", Platform = "PC", Valoration = 9 },

                new Videogame { Id = 6, Name = "asdadasda", Genre = "adasdad", Description = "adsadad", RegisterDate = new DateTime(2024, 6, 2, 22, 41, 43), Price = 30, Developer = "sasasasas", Platform = "assaasdas", Valoration = 9 }

            );

            modelBuilder.Entity<Operation>().HasData(
                
            new Operation { Id = 1, UserId = 2, VideogameId = 0, Concept = "Ingreso", Date = new DateTime(2024, 5, 6, 0, 9, 2), Quantity = 1, Amount = 100, Method = "PayPal" },
            new Operation { Id = 2, UserId = 2, VideogameId = 3, Concept = "Comprar Manor lords", Date = new DateTime(2024, 5, 6, 0, 9, 12), Quantity = 1, Amount = 0, Method = null },
            new Operation { Id = 3, UserId = 3, VideogameId = 0, Concept = "Ingreso", Date = new DateTime(2024, 5, 3, 20, 44, 36), Quantity = 1, Amount = 100, Method = "Tarjeta de crédito" },
            new Operation { Id = 4, UserId = 3, VideogameId = 0, Concept = "Retirada", Date = new DateTime(2024, 5, 3, 20, 44, 52), Quantity = 1, Amount = 30, Method = "PayPal" },
            new Operation { Id = 5, UserId = 3, VideogameId = 0, Concept = "Comprar Persona 5", Date = new DateTime(2024, 5, 5, 23, 33, 6), Quantity = 1, Amount = 50, Method = "Tarjeta de crédito" },
            new Operation { Id = 6, UserId = 3, VideogameId = 0, Concept = "Ingreso", Date = new DateTime(2024, 5, 5, 23, 39, 18), Quantity = 1, Amount = 50, Method = "Tarjeta de crédito" },
            new Operation { Id = 7, UserId = 3, VideogameId = 0, Concept = "Dragons dogma 2", Date = new DateTime(2024, 5, 5, 23, 39, 36), Quantity = 1, Amount = 70, Method = "Comprar Dragons dogma 2" },
            new Operation { Id = 8, UserId = 3, VideogameId = 0, Concept = "Ingreso", Date = new DateTime(2024, 5, 5, 23, 52, 22), Quantity = 1, Amount = 100, Method = "PayPal" },
            new Operation { Id = 9, UserId = 3, VideogameId = 2, Concept = "Comprar Final Fantasy XV", Date = new DateTime(2024, 5, 5, 23, 55, 20), Quantity = 0, Amount = 0, Method = null } );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }
    }
}
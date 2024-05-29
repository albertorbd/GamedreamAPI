using Gamedream.Models;
using Gamedream.Business;
using Gamedream.Data;
using Gamedream.Presentation;


IUserRepository userRepository = new UserRepository();
IVideogameRepository videogameRepository = new VideogameRepository();
IUserService userService = new UserService(userRepository);
IVideogameService videogameService = new VideogameService(videogameRepository);
Menu menu = new(userService, videogameService);
menu.Registration();
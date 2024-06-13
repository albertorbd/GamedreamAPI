using Microsoft.AspNetCore.Mvc;
using GamedreamAPI.Business;
using GamedreamAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GamedreamAPI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    
    public UsersController(ILogger<UsersController> logger, IUserService userService, IAuthService authService)
    {
        _logger = logger;
        _userService = userService;
        _authService= authService;

        
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try 
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to get the users. {ex.Message}");
            return BadRequest($"An error has ocurred trying to get the users. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("byEmail", Name = "GetUserByEmail")]
    public IActionResult GetUserByEmail(string email)
    {
        
        try
        {
            var user = _userService.GetUserByEmail(email);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"Couldnt find the user with email: {email}. {knfex.Message}");
           return NotFound($"Couldnt find the user with email: {email}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to get the user with email: {email}. {ex.Message}");
            return BadRequest($"An error has ocurred trying to get the user with email: {email}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpGet("{userId}", Name = "GetUserById") ]
    public IActionResult GetUserById(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"Couldnt find the user with id: {userId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with id: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to get the user with id: {userId}. {ex.Message}");
            return BadRequest($"An error has ocurred trying to get the user with id: {userId}. {ex.Message}");
        }
    }

   [Authorize(Roles = Roles.Admin + "," + Roles.User)]
   [HttpPut("{userId}")]

    public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDTO userUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

        try
        {
            _userService.UpdateUser(userId, userUpdate);
            return Ok($"El usuario con id: {userId} ha sido actualizado correctamente");
        }
         catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"Couldnt find the user with id: {userId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with id: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to update user with id: {userId}. {ex.Message}");
            return BadRequest($"An error has ocurred trying to update user with id: {userId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
        {return Forbid(); }

        try
        {
            _userService.DeleteUser(userId);
            return Ok($"El usuario con id {userId} ha sido eliminado ");
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"Couldnt find the user with id:: {userId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with id:: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to delete user with id: {userId}. {ex.Message}");
            return BadRequest($"An error has ocurred trying to delete user with id: {userId}. {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDTO userCreate)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExist = _userService.GetUserByEmail(userCreate.Email);
            if (userExist != null)
            {
                return BadRequest("El usuario ya está registrado.");
            }

            var user = _userService.RegisterUser(userCreate);

           
            return CreatedAtAction(nameof(GetAllUsers), new { userId = user.Id }, userCreate);
        }     
          catch (Exception ex)
        {
            _logger.LogError($"An error has ocurred trying to register the user. {ex.Message}");
            return BadRequest($"An error has ocurred trying to register the user. {ex.Message}");
        }
        
    }

}

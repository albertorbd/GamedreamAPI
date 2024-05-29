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
    
    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
        
    }

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
            _logger.LogError($"Error al obtener todos los usuarios. {ex.Message}");
            return BadRequest($"Error al obtener todos los usuarios. {ex.Message}");
        }
    }

    
    [HttpGet("Email")]
    public IActionResult GetUser(string email)
    {
        
        try
        {
            var user = _userService.GetUser(email);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con email: {email}. {knfex.Message}");
           return NotFound($"No se ha encontrado el usuario con email: {email}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el usuario con email: {email}. {ex.Message}");
            return BadRequest($"Error al obtener el usuario con email: {email}. {ex.Message}");
        }
    }

   
    /*
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try {
            _userService.UpdateUser(userId, userUpdateDTO);
            return NoContent();
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar el usuario con ID: {userId}. {ex.Message}");
            return BadRequest($"Error al actualizar el usuario con ID: {userId}. {ex.Message}");
        }
    }
*/
    
    [HttpDelete]
    public IActionResult DeleteUser(string email)
    {
        
        try
        {
            _userService.DeleteUser(email);
            return NoContent();
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con email: {email}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con email: {email}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar el usuario con email: {email}. {ex.Message}");
            return BadRequest($"Error al eliminar el usuario con email: {email}. {ex.Message}");
        }
    }

}

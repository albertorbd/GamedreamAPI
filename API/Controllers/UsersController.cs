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

    
    [HttpGet("Email", Name = "GetUserByEmail")]
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

   
   [HttpPut("Email")]

    public IActionResult UpdateUser(string userEmail, [FromBody] UserUpdateDTO userUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _userService.UpdateUser(userEmail, userUpdate);
            return Ok($"El usuario con email: {userEmail} ha sido actualizado correctamente");
        }
         catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con email: {userEmail}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con email: {userEmail}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar el usuario con email: {userEmail}. {ex.Message}");
            return BadRequest($"Error al actualizar el usuario con ID: {userEmail}. {ex.Message}");
        }
    }

    
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

            var userExist = _userService.GetUser(userCreate.Email);
            if (userExist != null)
            {
                return BadRequest("El usuario ya está registrado.");
            }

            var user = _userService.RegisterUser(userCreate.Name, userCreate.Lastname, userCreate.Email, userCreate.Password, userCreate.DNI,userCreate.BirthDate);

            // Retornar la acción exitosa junto con el nuevo usuario creado
            return CreatedAtAction(nameof(GetAllUsers), new { userId = user.Id }, userCreate);
        }     
          catch (Exception ex)
        {
            _logger.LogError($"Error al registrar el usuario. {ex.Message}");
            return BadRequest($"Error al registrar el usuario. {ex.Message}");
        }
        
    }

}

using Microsoft.AspNetCore.Mvc;
using GamedreamAPI.Business;
using GamedreamAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GamedreamAPI.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VideogamesController : ControllerBase
{
    private readonly ILogger<VideogamesController> _logger;
    private readonly IVideogameService _videogameService;
    
    public VideogamesController(ILogger<VideogamesController> logger, IVideogameService videogameService)
    {
        _logger = logger;
        _videogameService = videogameService;
        
    }

    [HttpGet]
    public ActionResult<IEnumerable<Videogame>> GetAllVideogames([FromQuery]VideogameQueryParameters videogameQueryParameters, bool orderByIdDesc)
    {

         if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            var videogames = _videogameService.GetAllVideogames(videogameQueryParameters, orderByIdDesc);
            return Ok(videogames);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todos los videojuegos. {ex.Message}");
            return BadRequest($"Error al obtener todos los videojuegos. {ex.Message}");
        }
    }


    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("Name", Name = "GetVideogameByName")]
    public IActionResult GetVideogame(string videogameName)
    {
        
        try
        {
            var videogame = _videogameService.GetVideogame(videogameName);
            return Ok(videogame);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
           return NotFound($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el videojuego con nombre: {videogameName}. {ex.Message}");
            return BadRequest($"Error al obtener el videojuego con nombre: {videogameName}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete]
    public IActionResult DeleteVideogame(string videogameName)
    {
        
        try
        {
            _videogameService.DeleteVideogame(videogameName);
            return NoContent();
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
            return NotFound($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar el videojuego con nombre: {videogameName}. {ex.Message}");
            return BadRequest($"Error al eliminar el videojuego con nombre: {videogameName}. {ex.Message}");
        }
    }

    [HttpPut("Name")]

    [Authorize(Roles = Roles.Admin)]
    public IActionResult UpdateVideogame(string videogameName, [FromBody] VideogameUpdateDTO videogameUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _videogameService.UpdateVideogame(videogameName, videogameUpdate);
            return Ok($"El usuario con nombre: {videogameName} ha sido actualizado correctamente");
        }
         catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
            return NotFound($"No se ha encontrado el videojuego con nombre: {videogameName}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar el videojuego con nombre: {videogameName}. {ex.Message}");
            return BadRequest($"Error al actualizar el videojuego con nombre: {videogameName}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateVideogame([FromBody] VideogameCreateDTO videogameCreate)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var videogameExist = _videogameService.GetVideogame(videogameCreate.Name);
            if (videogameExist != null)
            {
                return BadRequest("El usuario ya está registrado.");
            }

            var videogame = _videogameService.RegisterVideogame(videogameCreate);
            // Retornar la acción exitosa junto con el nuevo usuario creado
            return CreatedAtAction(nameof(GetAllVideogames), new { id = videogame.Id }, videogameCreate);
        }     
          catch (Exception ex)
        {
            _logger.LogError($"Error al registrar el usuario. {ex.Message}");
            return BadRequest($"Error al registrar el usuario. {ex.Message}");
        }
        
}
}

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
    [HttpGet("byName", Name = "GetVideogameByName")]
    public IActionResult GetVideogameByName(string videogameName)
    {
        
        try
        {
            var videogame = _videogameService.GetVideogameByName(videogameName);
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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{videogameId}", Name = "GetVideogameById") ]
    public IActionResult GetVideogameById(int videogameId)
    {
        
        try
        {
            var videogame = _videogameService.GetVideogameById(videogameId);
            return Ok(videogame);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
           return NotFound($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el videojuego con id: {videogameId}. {ex.Message}");
            return BadRequest($"Error al obtener el videojuego con id: {videogameId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{videogameId}")]
    public IActionResult DeleteVideogame(int videogameId)
    {
        
        try
        {
            _videogameService.DeleteVideogame(videogameId);
            return Ok($"El videojuego con id {videogameId} ha sido eliminado correctamente");
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar el videojuego con id: {videogameId}. {ex.Message}");
            return BadRequest($"Error al eliminar el videojuego con id: {videogameId}. {ex.Message}");
        }
    }

    [HttpPut("{videogameId}")]

    [Authorize(Roles = Roles.Admin)]
    public IActionResult UpdateVideogame(int videogameId, [FromBody] VideogameUpdateDTO videogameUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _videogameService.UpdateVideogame(videogameId, videogameUpdate);
            return Ok($"El videojuego con id: {videogameId} ha sido actualizado correctamente");
        }
         catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el videojuego con id: {videogameId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar el videojuego con id: {videogameId}. {ex.Message}");
            return BadRequest($"Error al actualizar el videojuego con id: {videogameId}. {ex.Message}");
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

            var videogameExist = _videogameService.GetVideogameByName(videogameCreate.Name);
            if (videogameExist != null)
            {
                return BadRequest("El videojuego ya está registrado.");
            }

            var videogame = _videogameService.RegisterVideogame(videogameCreate);
            // Retornar la acción exitosa junto con el nuevo usuario creado
            return CreatedAtAction(nameof(GetAllVideogames), new { id = videogame.Id }, videogameCreate);
        }     
          catch (Exception ex)
        {
            _logger.LogError($"Error al registrar el videojuego. {ex.Message}");
            return BadRequest($"Error al registrar el videojuego. {ex.Message}");
        }
        
}
}

using Microsoft.AspNetCore.Mvc;
using GamedreamAPI.Business;
using GamedreamAPI.Models;

using Microsoft.AspNetCore.Authorization;

namespace GamedreamAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
    

        private readonly IOperationService _operationService;

        private readonly IAuthService _authService;
         private readonly ILogger<AuthController> _logger;


        public OperationsController(ILogger<AuthController> logger, IAuthService authService, IOperationService operationService)
        {
            _logger=logger;         
            _authService=authService;
            _operationService=operationService;
        }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("Deposit")]
    public IActionResult MakeDeposit([FromBody] MoneyTransferDTO moneyTransferDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(moneyTransferDTO.UserId), HttpContext.User)) 
            {return Forbid(); }

        try {
            _operationService.MakeDeposit(moneyTransferDTO);
            return Ok("Depósito realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {moneyTransferDTO.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {moneyTransferDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer el depósito del usuario con ID: {moneyTransferDTO.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer el depósito del usuario con ID: {moneyTransferDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("WithDrawal")]
    public IActionResult MakeWithDrawal([FromBody] MoneyTransferDTO moneyTransferDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(moneyTransferDTO.UserId), HttpContext.User)) 
            {return Forbid(); }

        try {
            _operationService.MakeWithdrawal(moneyTransferDTO);
            return Ok("Retirada realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {moneyTransferDTO.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {moneyTransferDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer el depósito del usuario con ID: {moneyTransferDTO.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer el depósito del usuario con ID: {moneyTransferDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("BuyVideogame")]
    public IActionResult BuyVideogame([FromBody] BuyVideogameDTO buyVideogameDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buyVideogameDTO.UserId), HttpContext.User)) 
            {return Forbid(); }

        try {
            _operationService.BuyVideogame(buyVideogameDTO);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {buyVideogameDTO.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {buyVideogameDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer la compra del usuario con ID: {buyVideogameDTO.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer la compra del usuario con ID: {buyVideogameDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet]
    public ActionResult<IEnumerable<Operation>> GetOperations([FromQuery] OperationQueryParameters operationQueryParameters)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(operationQueryParameters.UserId), HttpContext.User)) 
            {return Forbid(); }

        try {
            var operations = _operationService.GetAllOperations(operationQueryParameters);
            return Ok(operations);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todas las operaciones. {ex.Message}");
            return BadRequest($"Error al obtener todas las operaciones. {ex.Message}");
        }
    }


    [Authorize(Roles = Roles.Admin + "," + Roles.User)] 
        [HttpGet("{userId}/purchasedvideogames")]
     public IActionResult GetPurchasedVideogames(int userId)
        {
            try
            {
                var purchasedVideogames = _operationService.VideogamesPurchased(userId);
                return Ok(purchasedVideogames);
            }
            catch (Exception ex)
            {
                // En caso de error, devuelve un mensaje de error
                return StatusCode(500, $"Error al obtener los videojuegos comprados: {ex.Message}");
            }
        }
    }


}
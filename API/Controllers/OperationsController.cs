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
    [HttpPost("deposit")]
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
            _logger.LogWarning($"Couldnt find the user with ID: {moneyTransferDTO.UserId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with ID: {moneyTransferDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error trying to make the deposit to the user with ID: {moneyTransferDTO.UserId}. {ex.Message}");
            return BadRequest($"Error trying to make the deposit to the user with ID: {moneyTransferDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("withdrawal")]
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
            _logger.LogWarning($"Couldnt find the user with ID: {moneyTransferDTO.UserId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with ID: {moneyTransferDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error trying to make the withdraw to the user with ID: {moneyTransferDTO.UserId}. {ex.Message}");
            return BadRequest($"Error trying to make the withdraw to the user with ID: {moneyTransferDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("buyVideogame")]
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
            _logger.LogWarning($"Couldnt find the user with ID: {buyVideogameDTO.UserId}. {knfex.Message}");
            return NotFound($"Couldnt find the user with ID: {buyVideogameDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error making the purchase of the user with ID: {buyVideogameDTO.UserId}. {ex.Message}");
            return BadRequest($"Error making the purchase of the user with ID: {buyVideogameDTO.UserId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Operation>> GetOperations( int userId, [FromQuery]OperationQueryParameters operationQueryParameters)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

        try {
            var operations = _operationService.GetAllOperations( userId, operationQueryParameters);
            return Ok(operations);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error getting all operations. {ex.Message}");
            return BadRequest($"Error getting all operations. {ex.Message}");
        }
    }


    [Authorize(Roles = Roles.Admin + "," + Roles.User)] 
        [HttpGet("{userId}/videogames")]
     public IActionResult GetPurchasedVideogames(int userId)
        {
            if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

            try
            {
                var purchasedVideogames = _operationService.VideogamesPurchased(userId);
                return Ok(purchasedVideogames);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error getting the videogames of the user: {ex.Message}");
            }
        }
    }


}
using Microsoft.AspNetCore.Mvc;
using GamedreamAPI.Business;

namespace GamedreamAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
         private readonly ILogger<AuthController> _logger;


        public AuthController(IUserService userService, IAuthService authService, ILogger<AuthController> logger)
        {
            _logger=logger;
            _userService = userService;
            _authService=authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO userDto)
        {

        if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
            try
        {
            var user = _userService.loginCheck(userDto.Email, userDto.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("No se ha encontrado ningún usuario con esas credenciales");
            }
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No user found. {knfex.Message}");
           return NotFound($"No user found. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"No user found. {ex.Message}");
            return BadRequest($"No user found. {ex.Message}");
        }
}

}
}
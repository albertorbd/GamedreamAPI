using System.Security.Claims;
using GamedreamAPI.Models;

namespace GamedreamAPI.Business
{
    public interface IAuthService
    {
       
        public string GenerateJwtToken(User user);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    }
}
using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace ItIsPizzaDay.Server.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;

    [Authorize]
    public class AuthenticatedUserController : DefaultController
    {
        readonly IHttpContextAccessor _http;

        public AuthenticatedUserController(IHttpContextAccessor http)
        {
            _http = http;
        }

        public async Task<AuthenticatedUser> GetByToken()
        {
            var tokenText = await _http.HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var expiration = User.GetExpiration();
            
            var token = new Token(tokenText, expiration);

            var role = User.GetIsAdmin() 
                ? Role.Admin 
                : Role.User;
            
            return new AuthenticatedUser(User.TryGetName(), token, role);
        }
    }
}
using System.Net.Http;
using System.Threading.Tasks;
using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Client.Services
{
    public class AuthenticatedUserEndPoint :  ReadEndPoint<AuthenticatedUser>
    {
        public AuthenticatedUserEndPoint(HttpClient http, ApiConfig config, AuthService authService) : base(http, config, authService)
        {
        }
        
        public Task<AuthenticatedUser> GetByTokenAsync() 
            => GetJson<AuthenticatedUser>($"GetByToken");
    }
}
using System.Net.Http;
using System.Threading.Tasks;
using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Client.Services
{
    public class AuthenticatedUserEndPoint :  ReadEndPoint<AuthenticatedUser>
    {
        public AuthenticatedUserEndPoint(HttpClient http, ApiConfig config, ITokenSource tokenSource) : base(http, config, tokenSource)
        {
        }
        
        public Task<AuthenticatedUser> GetByTokenAsync() 
            => GetJson<AuthenticatedUser>($"GetByToken");
    }
}
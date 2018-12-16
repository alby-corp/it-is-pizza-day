namespace ItIsPizzaDay.Client.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class EndPoint
    {
        protected readonly HttpClient _http;
        protected readonly AuthService _authService;

        public EndPoint(HttpClient http, AuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        protected async Task SetAuthorizationHeaders()
        {
            var user = await _authService.TryGetUserAsync();

            if (user != null)
            {
                _http.DefaultRequestHeaders.Remove("Authorization");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Token.Value}");
            }
        }
    }
}
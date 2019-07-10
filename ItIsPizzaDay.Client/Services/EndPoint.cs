namespace ItIsPizzaDay.Client.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class EndPoint
    {
        protected readonly HttpClient _http;
        readonly ITokenSource _tokenSource;

        public EndPoint(HttpClient http, ITokenSource tokenSource)
        {
            _http = http;
            _tokenSource = tokenSource;
        }

        protected async Task SetAuthorizationHeaders()
        {
            var token = await _tokenSource.TryGetTokenAsync();

            if (token != null)
            {
                _http.DefaultRequestHeaders.Remove("Authorization");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
            }
        }
    }
}
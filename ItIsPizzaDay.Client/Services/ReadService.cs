using System.Net.Http;
using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Client.Services.Abstract;
using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Client.Services
{
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ReadService : IReadService
    {
        private readonly ITokenSource _tokenSource;
        private readonly ApiConfig _config;
        private readonly HttpClient _http;

        public ReadService(HttpClient http, ApiConfig config, ITokenSource tokenSource)
        {
            _http = http;
            _config = config;
            _tokenSource = tokenSource;
        }

        public AuthenticatedUserEndPoint User => new AuthenticatedUserEndPoint(_http, _config, _tokenSource);
        public ReadEndPoint<Food> Food => new ReadEndPoint<Food>(_http, _config, _tokenSource);
        public ReadEndPoint<Ingredient> Ingredient => new ReadEndPoint<Ingredient>(_http, _config, _tokenSource);
        public ReadEndPoint<FoodType> Type => new ReadEndPoint<FoodType>(_http, _config, _tokenSource);
        public OrderReadEndPoint Order => new OrderReadEndPoint(_http, _config, _tokenSource);
    }

    public class ReadServiceFactory
    {
        private readonly ApiConfig _config;
        private readonly HttpClient _http;
        
        public ReadServiceFactory(HttpClient http, ApiConfig config)
        {
            _http = http;
            _config = config;
        }
        
        public IReadService Create(ITokenSource tokenSource) => new ReadService(_http, _config, tokenSource);
    }
}
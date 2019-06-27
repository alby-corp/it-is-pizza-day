using System.Net.Http;
using ItIsPizzaDay.Client.Models;
using ItIsPizzaDay.Client.Services.Abstract;
using ItIsPizzaDay.Shared.Models;

namespace ItIsPizzaDay.Client.Services
{
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ReadService : IReadService
    {
        private readonly AuthService _authService;
        private readonly ApiConfig _config;
        private readonly HttpClient _http;

        public ReadService(HttpClient http, ApiConfig config, AuthService authService)
        {
            _http = http;
            _config = config;
            _authService = authService;
        }

        public AuthenticatedUserEndPoint User => new AuthenticatedUserEndPoint(_http, _config, _authService);
        public ReadEndPoint<Food> Food => new ReadEndPoint<Food>(_http, _config, _authService);
        public ReadEndPoint<Ingredient> Ingredient => new ReadEndPoint<Ingredient>(_http, _config, _authService);
        public ReadEndPoint<FoodType> Type => new ReadEndPoint<FoodType>(_http, _config, _authService);
        public OrderReadEndPoint Order => new OrderReadEndPoint(_http, _config, _authService);
    }
}
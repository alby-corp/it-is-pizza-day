namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Net.Http;
    using Abstract;
    using ItIsPizzaDay.Shared.Models;
    using Models;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ReadService : IReadService
    {
        private readonly HttpClient _http;
        private readonly ApiConfig _config;
        private readonly AuthService _authService;

        public ReadService(HttpClient http, ApiConfig config, AuthService authService)
        {
            _http = http;
            _config = config;
            _authService = authService;
        }

        public ReadEndPoint<Food> Food => new ReadEndPoint<Food>(_http, _config, _authService);
        public ReadEndPoint<Ingredient> Ingredient => new ReadEndPoint<Ingredient>(_http, _config, _authService);
        public ReadEndPoint<FoodType> Type => new ReadEndPoint<FoodType>(_http, _config, _authService);
        public OrderReadEndPoint Order => new OrderReadEndPoint(_http, _config, _authService);
    }
}
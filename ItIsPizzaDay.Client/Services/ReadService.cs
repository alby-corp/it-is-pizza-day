namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Net.Http;
    using Abstract;
    using ItIsPizzaDay.Shared.Models;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ReadService : IReadService
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;
        private readonly AuthService _authService;

        public ReadService(HttpClient http, Uri baseUrl, AuthService authService)
        {
            _http = http;
            _baseUrl = baseUrl;
            _authService = authService;
        }

        public ReadEndPoint<Food> Food => new ReadEndPoint<Food>(_http, _baseUrl, _authService);
        public ReadEndPoint<Ingredient> Ingredient => new ReadEndPoint<Ingredient>(_http, _baseUrl, _authService);
        public ReadEndPoint<FoodType> Type => new ReadEndPoint<FoodType>(_http, _baseUrl, _authService);
        public OrderReadEndPoint Order => new OrderReadEndPoint(_http, _baseUrl, _authService);
    }
}
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

        public ReadService(HttpClient http, Uri baseUrl)
        {
            _http = http;
            _baseUrl = baseUrl;
        }

        public ReadEndPoint<Food> Food => new ReadEndPoint<Food>(_http, _baseUrl);
        public ReadEndPoint<Ingredient> Ingredient => new ReadEndPoint<Ingredient>(_http, _baseUrl);
        public ReadEndPoint<FoodType> Type => new ReadEndPoint<FoodType>(_http, _baseUrl);
    }
}
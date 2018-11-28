namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using FoodType = ItIsPizzaDay.Shared.Models.Type;

    public class ReadService : IReadService
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;

        public ReadService(HttpClient http, string baseUrl)
        {
            _http = http;
            _baseUrl = new Uri($"{baseUrl}/api");
        }

        public Task<IEnumerable<FoodType>> Types() => GetAsync<FoodType>();

        public Task<IEnumerable<Ingredient>> Ingredients() => GetAsync<Ingredient>();

        public Task<IEnumerable<Food>> Foods() => GetAsync<Food>();

        private Task<IEnumerable<T>> GetAsync<T>() => _http.GetJsonAsync<IEnumerable<T>>($@"{_baseUrl}/{typeof(T).Name}/GetAll");
    }
}
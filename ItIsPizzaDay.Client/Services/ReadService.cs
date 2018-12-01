namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public Task<ICollection<FoodType>> Types() => GetAllAsync<FoodType>();

        public Task<ICollection<Ingredient>> Ingredients() => GetAllAsync<Ingredient>();

        public Task<Food> Food(Guid id) => GetAsync<Food>(id);

        public Task<ICollection<Food>> Foods() => GetAllAsync<Food>();

        private async Task<T> GetAsync<T>(Guid id) => await _http.GetJsonAsync<T>($@"{_baseUrl}/{typeof(T).Name}/Get/{id}");

        private async Task<ICollection<T>> GetAllAsync<T>() => (await _http.GetJsonAsync<ICollection<T>>($@"{_baseUrl}/{typeof(T).Name}/GetAll")).ToList();
    }
}
namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;

    public class ReadService : IReadService
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;

        public ReadService(HttpClient http, string baseUrl)
        {
            _http = http;
            _baseUrl = new Uri($"{baseUrl}/api");
        }

        public Task<IEnumerable<ItIsPizzaDay.Shared.Models.Type>> Types() => GetAsync<ItIsPizzaDay.Shared.Models.Type>();

        private Task<IEnumerable<T>> GetAsync<T>() => _http.GetJsonAsync<IEnumerable<T>>($@"{_baseUrl}/{typeof(T).Name}/GetAll");
    }
}
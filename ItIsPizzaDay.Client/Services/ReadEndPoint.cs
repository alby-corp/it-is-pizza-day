namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;

    public class ReadEndPoint<T>
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;

        public ReadEndPoint(HttpClient http, Uri baseUrl)
        {
            _http = http;
            _baseUrl = baseUrl;
        }

        public async Task<T> GetAsync(Guid id) => await _http.GetJsonAsync<T>($@"{_baseUrl}/{typeof(T).Name}/Get/{id}");

        public async Task<ICollection<T>> GetAllAsync() => (await _http.GetJsonAsync<ICollection<T>>($@"{_baseUrl}/{typeof(T).Name}/GetAll")).ToList();
    }
}
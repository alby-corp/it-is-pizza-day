namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Models;

    public class ReadEndPoint<T> : EndPoint
    {
        private readonly Uri _baseUrl;

        public ReadEndPoint(HttpClient http, ApiConfig config, AuthService authService)
            : base(http, authService)
        {
            _baseUrl = config.BaseUrl;
        }

        public Task<T> GetAsync(Guid id) 
            => GetJson<T>($"Get/{id}");

        public Task<ICollection<T>> GetAllAsync() 
            => GetJson<ICollection<T>>("GetAll");
        
        protected async Task<TResult> GetJson<TResult>(string method)
        {
            await SetAuthorizationHeaders();
            return await _http.GetJsonAsync<TResult>($@"{_baseUrl}/{typeof(T).Name}/{method}");
        }
    }
}
namespace ItIsPizzaDay.Client.Services.Abstract
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Abstract;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor;

    public class WriteEndPoint<T>
        where T : Entity, IEntity
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;
        private readonly AuthService _authService;

        public WriteEndPoint(HttpClient http, Uri baseUrl, AuthService authService)
        {
            _http = http;
            _baseUrl = baseUrl;
            this._authService = authService;
        }

        public async Task Save(T entity)
        {
            await SetAuthorizationHeaders();
           
            var url = $"{_baseUrl.AbsoluteUri}/{typeof(T).Name}";
            
            if (entity.Id == Guid.Empty)
            {
                await _http.SendJsonAsync<Entity>(HttpMethod.Post, $"{url}/Create", entity);
            }
            else
            {
                await _http.SendJsonAsync<Entity>(HttpMethod.Put, $"{url}/Update", entity);
            }
        }
        
        public async Task Delete(Guid id)
        {
            await SetAuthorizationHeaders();
            
            await _http.DeleteAsync($"{_baseUrl}/{typeof(T).Name}/Delete/{id}");
        }

        private async Task SetAuthorizationHeaders()
        {
            var user = await _authService.TryGetUserAsync();

            if (user != null)
            {
                _http.DefaultRequestHeaders.Remove("Authorization");
                _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Token.Value}");
            }
        }
    }
}
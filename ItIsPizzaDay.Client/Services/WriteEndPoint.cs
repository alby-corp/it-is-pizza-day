namespace ItIsPizzaDay.Client.Services.Abstract
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ItIsPizzaDay.Shared.Abstract;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor;
    using Models;

    public class WriteEndPoint<T> : EndPoint
        where T : Entity, IEntity
    {
        private readonly Uri _baseUrl;

        public WriteEndPoint(HttpClient http, ApiConfig config, AuthService authService)
        :base(http, authService)
        {
            _baseUrl = config.BaseUrl;
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
    }
}
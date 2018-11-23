namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    
    public class ResourceService<T>
    {
        private HttpClient _http;
        private Uri _origin;

        public ResourceService(HttpClient http, Uri origin)
        {
            _http = http;
            _origin = origin;
        }

        public IEnumerable<T> GetAll()
        {
            return null;
        }
        
    }
}
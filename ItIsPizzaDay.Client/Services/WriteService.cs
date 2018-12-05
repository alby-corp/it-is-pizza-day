namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Net.Http;
    using Abstract;
    using ItIsPizzaDay.Shared.Models;

    public class WriteService : IWriteService
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUrl;

        public WriteService(HttpClient http, Uri baseUrl)
        {
            _http = http;
            _baseUrl = baseUrl;
        }

        public WriteEndPoint<Order> Order => new WriteEndPoint<Order>(_http, _baseUrl);
    }
}
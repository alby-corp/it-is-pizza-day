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
        private readonly AuthService _authService;

        public WriteService(HttpClient http, Uri baseUrl, AuthService authService)
        {
            _http = http;
            _baseUrl = baseUrl;
            _authService = authService;
        }

        public WriteEndPoint<Order> Order => new WriteEndPoint<Order>(_http, _baseUrl, _authService);
    }
}
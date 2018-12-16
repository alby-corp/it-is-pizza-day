namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Net.Http;
    using Abstract;
    using ItIsPizzaDay.Shared.Models;
    using Models;

    public class WriteService : IWriteService
    {
        private readonly HttpClient _http;
        private readonly ApiConfig _config;
        private readonly AuthService _authService;

        public WriteService(HttpClient http, ApiConfig config, AuthService authService)
        {
            _http = http;
            _config = config;
            _authService = authService;
        }

        public WriteEndPoint<Order> Order => new WriteEndPoint<Order>(_http, _config, _authService);
    }
}
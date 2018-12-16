namespace ItIsPizzaDay.Client.Models
{
    using System;

    public class ApiConfig
    {
        public ApiConfig(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        public Uri BaseUrl { get; }
    }
}
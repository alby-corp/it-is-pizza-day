namespace ItIsPizzaDay.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Blazor.Extensions.Storage;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Abstract;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var baseUrl = new Uri("http://localhost:5000/api");

            services.AddStorage();
            services.AddSingleton<IReadService>(provider => new ReadService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<IWriteService>(provider => new WriteService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<ICartService>(provider => new CartService(provider.GetRequiredService<LocalStorage>(),"f71cf4e3-a9b1-4852-a893-9f71a6399b4b"));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
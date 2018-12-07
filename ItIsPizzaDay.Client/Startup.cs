namespace ItIsPizzaDay.Client
{
    using System;
    using System.Net.Http;
    using Blazor.Extensions.Storage;
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
            services.AddSingleton<IReadService, ReadService>(provider => new ReadService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<IWriteService, WriteService>(provider => new WriteService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<ICartService, CartService>(provider => new CartService(provider.GetRequiredService<LocalStorage>()));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
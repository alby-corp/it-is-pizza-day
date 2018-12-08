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
            services.AddSingleton<IReadService, ReadService>(provider => new ReadService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<IWriteService, WriteService>(provider => new WriteService(provider.GetRequiredService<HttpClient>(), baseUrl));
            services.AddSingleton<ICartService, CartService>(provider => new CartService(
                provider.GetRequiredService<LocalStorage>(),
                async () =>
                {
                    Console.WriteLine("QUI");
                    var t =  await provider.GetRequiredService<LocalStorage>().GetItem<ICollection<FoodOrder>>("f71cf4e3-a9b1-4852-a893-9f71a6399b4b");
                    Console.WriteLine("QUA");
                    return t;
                },
                "f71cf4e3-a9b1-4852-a893-9f71a6399b4b"
            ));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
namespace ItIsPizzaDay.Client
{
    using System.Collections.Generic;
    using System.Net.Http;
    using ItIsPizzaDay.Shared.Models;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IReadService, ReadService>(provider => new ReadService(provider.GetRequiredService<HttpClient>(), "http://localhost:5000"));
            services.AddSingleton<CartService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
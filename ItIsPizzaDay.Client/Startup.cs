namespace ItIsPizzaDay.Client
{
    using System;
    using System.Net.Http;
    using Blazor.Extensions.Storage;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Services;
    using Services.Abstract;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddSingleton<IReadService, ReadService>();
            services.AddSingleton<IWriteService, WriteService>();
            services.AddSingleton<ICartService>(provider => new CartService(provider.GetRequiredService<LocalStorage>(),"f71cf4e3-a9b1-4852-a893-9f71a6399b4b"));
            services.AddSingleton<IAlby, Alby>();
            
            services.AddSingleton(provider =>
            {
                var uri = provider.GetRequiredService<IUriHelper>();
                return new AuthConfig(
                    "97b8377a-5c1c-4b1c-a630-6299b71718fe",
                    new[] { "openid" },
                    $"{uri.GetBaseUri()}signed-in",
                    uri.GetBaseUri(),
                    "localStorage");
            });
            
            services.AddSingleton(provider =>
            {
                var uri = provider.GetRequiredService<IUriHelper>();
                return new ApiConfig($"{uri.GetBaseUri()}api");
            });
            
            services.AddSingleton<AuthService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
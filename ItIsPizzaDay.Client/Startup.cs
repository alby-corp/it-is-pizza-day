using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ItIsPizzaDay.Client
{
    using System.Net.Http;
    using Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IReadService, ReadService>((provider) => new ReadService(provider.GetRequiredService<HttpClient>(), "http://localhost:5000"));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}

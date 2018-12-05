namespace ItIsPizzaDay.Server.Middlewares
{
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;

    public static class Middelwares
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<TypeRepository>();
            services.AddScoped<FoodRepository>();
            services.AddScoped<IngredientRepository>();
            services.AddScoped<OrderRepository>();
        }
    }
}
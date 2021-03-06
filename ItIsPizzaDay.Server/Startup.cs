namespace ItIsPizzaDay.Server
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mime;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Blazor.Server;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Middlewares;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.CheckAdditionalContent = true;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<QueenMargheritaContext>(options =>
            {
                var heroku = Heroku.TryParseConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"));
                var local = "Host=localhost;Port=5555;Database=QueenMargherita;Username=SamuraiTeam;Password=SamuraiTeam";
                
                var connectionString = heroku ?? local;
                
                options.UseNpgsql(connectionString);
            });

            services.AddRepositories();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://login.microsoftonline.com/e64bac42-73e6-48b8-abae-54b44ceed0da/v2.0";
                    options.Audience  = "97b8377a-5c1c-4b1c-a630-6299b71718fe";
                    options.SaveToken = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/api", api =>
            {
                api.UseAuthentication();
                api.UseMvc(routes =>
                {
                    routes.MapRoute("default", "{controller}/{action}/{id?}");
                });
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
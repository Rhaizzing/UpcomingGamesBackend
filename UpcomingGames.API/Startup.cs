using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UpcomingGames.API.Repositories;
using UpcomingGames.API.Services;
using UpcomingGames.API.Utils;
using UpcomingGames.Database;
using UpcomingGames.Sources.Implementations;
using UpcomingGames.Sources.Utils;
using UpcomingGamesBackend.Model.Interfaces;

namespace UpcomingGames.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "cors",
                    builder =>
                    {
                        builder.WithOrigins("*");
                    });
            });
            
            services.AddDbContext<postgresContext>();
            
            services.AddSingleton(c => new IGDBClient(
                // Found in Twitch Developer portal for your app
                Environment.GetEnvironmentVariable("TWITCH_CLIENT_TOKEN"),
                Environment.GetEnvironmentVariable("TWITCH_CLIENT_SECRET"),
                new JsonTokenStore("token.json")
            ));
            services.AddSingleton<IgdbSource>();

            services.AddTransient<IGameRepository, GameRepository>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpcomingGames.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UpcomingGames.API v1"));
            }
            
            app.UseRouting();

            app.UseCors("cors");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

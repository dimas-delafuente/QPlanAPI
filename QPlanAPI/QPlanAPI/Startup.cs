using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QPlanAPI.Core;
using QPlanAPI.Infrastructure;
using QPlanAPI.DataAccess;
using QPlanAPI.Presenters;
using QPlanAPI.Config;
using System.IO;

namespace QPlanAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMongoDbClient(new DbSettings
            {
                ConnectionString = Configuration.GetSection("MongoDbSettings:ConnectionString").Value,
                DatabaseName = Configuration.GetSection("MongoDbSettings:DatabaseName").Value,
                DatabaseCollections = new DbSettings.DbCollections
                {
                    Restaurants = Configuration.GetSection("MongoDbSettings:DatabaseCollections:Restaurants").Value
                }
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddApplicationCore();
            services.AddApplicationInfrastructure();

            services.Configure<ExternalRestaurantsConfig>(GetExternalRestaurantsConfiguration());

            services.AddSingleton<RestaurantPresenter>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private IConfiguration GetExternalRestaurantsConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\Config"))
            .AddJsonFile(@"externalrestaurantssettings.json");

            IConfigurationRoot configuration = builder.Build();

            return configuration;
        }
    }
}

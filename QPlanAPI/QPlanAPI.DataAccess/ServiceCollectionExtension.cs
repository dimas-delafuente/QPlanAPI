using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QPlanAPI.Core;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.DataAccess.Contexts;
using QPlanAPI.DataAccess.Repositories;

namespace QPlanAPI.DataAccess
{
    public static class ServiceCollectionExtension
    {
        public static void AddMongoDbClient(this IServiceCollection services, DbSettings dbSettings)
        {
            services.Configure<DbSettings>(
                options =>
                {
                    options.ConnectionString = dbSettings.ConnectionString;
                    options.DatabaseName = dbSettings.DatabaseName;
                    options.DatabaseCollections = dbSettings.DatabaseCollections;
                });

            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(dbSettings.ConnectionString));

            //DB Contexts
            services.InitializeDbContexts();
        }

        private static void InitializeDbContexts(this IServiceCollection services) {
            services.AddScoped<IRestaurantContext, RestaurantContext>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        }
    }
}

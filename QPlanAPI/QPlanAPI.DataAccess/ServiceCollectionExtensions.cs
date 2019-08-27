using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QPlanAPI.Core;

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
                });

            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(dbSettings.ConnectionString));

            //DB Contexts
        }
    }
}

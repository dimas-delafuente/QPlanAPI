using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<IDocumentClient, DocumentClient>(
                _ => new DocumentClient(new System.Uri("https://qplandb.documents.azure.com:10255"), "aRlOGoG0Tx6xCPMaPCdE4ShvnHKmVUp3PezUEgUZjklq8JtGzxGgrqihx38n7Ql2MLOebf9h3f1iNEG9e7C3GA=="));

            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(dbSettings.ConnectionString));


            //DB Contexts
            services.InitializeDbContexts();
        }

        private static void InitializeDbContexts(this IServiceCollection services) {
            services.AddScoped<IRestaurantContext, RestaurantContext>();
            services.AddScoped<IRestaurantContext, AzureRestaurantContext>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        }
    }
}

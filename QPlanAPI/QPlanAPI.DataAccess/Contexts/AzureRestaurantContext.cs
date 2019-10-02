using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QPlanAPI.Core;
using QPlanAPI.DataAccess.Entities;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Spatial;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using QPlanAPI.Domain.Restaurants;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace QPlanAPI.DataAccess.Contexts
{
    public class AzureRestaurantContext : IRestaurantContext
    {

        private readonly Uri restaurantCollection;


        private readonly IDocumentClient _azureClient;

        public AzureRestaurantContext(IOptions<DbSettings> options, IDocumentClient client)
        {
            _azureClient = client;
            restaurantCollection = UriFactory.CreateDocumentCollectionUri(options.Value.DatabaseName, options.Value.DatabaseCollections.Restaurants);
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByRestaurantType(RestaurantType type)
        {
            throw new NotImplementedException();
        }

        public Task<RestaurantEntity> GetRestaurantById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RestaurantEntity>> GetRestaurants()
        {
            try
            {
                DocumentClient client = new DocumentClient(new Uri("https://qplandb.documents.azure.com:10255"), "aRlOGoG0Tx6xCPMaPCdE4ShvnHKmVUp3PezUEgUZjklq8JtGzxGgrqihx38n7Ql2MLOebf9h3f1iNEG9e7C3GA==");
                Database db1 = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = "QPlanDb" });

                Database db = await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("QPlanDb"));
                var result = await client.ReadDocumentCollectionFeedAsync(restaurantCollection);
                return (List<RestaurantEntity>)(dynamic)result;
            }
            catch
            {

            }

            return null;
        }

        public Task<List<RestaurantLocationEntity>> GetRestaurantsByLocation(double longitude, double latitude, double radius)
        {
            throw new NotImplementedException();
        }

        public Task Insert(RestaurantEntity restaurant)
        {
            throw new NotImplementedException();
        }

        public Task InsertMany(List<RestaurantEntity> restaurant)
        {
            throw new NotImplementedException();
        }

        public Task Update(RestaurantEntity restaurant)
        {
            throw new NotImplementedException();
        }
    }
}

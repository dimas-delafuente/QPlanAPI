using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QPlanAPI.Core;
using QPlanAPI.DataAccess.Entities;

namespace QPlanAPI.DataAccess.Contexts
{
    public class RestaurantContext : IRestaurantContext
    {

        private readonly IMongoDatabase _db;
        private readonly string restaurantCollection;

        public RestaurantContext(IOptions<DbSettings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.DatabaseName);
            restaurantCollection = options.Value.DatabaseCollections.Restaurants;
        }

        public IMongoCollection<RestaurantEntity> Restaurants => _db.GetCollection<RestaurantEntity>(restaurantCollection);
    }
}

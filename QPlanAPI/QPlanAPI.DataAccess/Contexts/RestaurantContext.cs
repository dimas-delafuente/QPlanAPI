using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QPlanAPI.Core;
using QPlanAPI.DataAccess.Entities;
using System.Threading.Tasks;
using MongoDB.Bson;
using QPlanAPI.Domain.Restaurants;

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

        public async Task<RestaurantEntity> GetRestaurantById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<RestaurantEntity>> GetRestaurants()
        {
            return await Restaurants.FindAsync(_ => true)
                            .Result.ToListAsync();
        }

        public async Task<List<RestaurantLocationEntity>> GetRestaurantsByLocation(double longitude, double latitude, double radius)
        {
            return await Restaurants.Aggregate<RestaurantLocationEntity>(GetGeoNearQuery(longitude, latitude, radius)).ToListAsync();
        }

        public async Task Insert(RestaurantEntity restaurant)
        {
            await Restaurants.InsertOneAsync(restaurant);
        }

        public async Task InsertMany(List<RestaurantEntity> restaurants)
        {
            await Restaurants.InsertManyAsync(restaurants);
        }


        public async Task Delete(string id)
        {
            await Restaurants.FindOneAndDeleteAsync(r => r.Id == id);
        }

        public async Task DeleteByRestaurantType(RestaurantType type)
        {
            await Restaurants.DeleteManyAsync(r => r.Type.Equals(type));
        }


        public async Task Update(RestaurantEntity restaurant)
        {
            throw new System.NotImplementedException();
        }



        #region "Private Methods"

        private List<BsonDocument> GetGeoNearQuery(double longitude, double latitude, double radius)
        {
            var geoNearOptions = new BsonDocument {
                { "near", new BsonDocument {
                    { "type", "Point" },
                    { "coordinates", new BsonArray {longitude, latitude} },
                } },
                { "distanceField", "Distance" },
                { "maxDistance", radius},
                { "spherical" , true }
            };

            var geoNearQuery = new List<BsonDocument>{
                new BsonDocument { { "$geoNear", geoNearOptions } }
            };

            return geoNearQuery;
        }

        Task IRestaurantContext.InsertMany(List<RestaurantEntity> restaurant)
        {
            throw new System.NotImplementedException();
        }

        Task IRestaurantContext.Update(RestaurantEntity restaurant)
        {
            throw new System.NotImplementedException();
        }

        Task IRestaurantContext.Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        Task IRestaurantContext.DeleteByRestaurantType(RestaurantType type)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.DataAccess.Contexts;
using QPlanAPI.DataAccess.Entities;
using QPlanAPI.Domain;
using QPlanAPI.Domain.Restaurants;
using MongoDB.Bson;


namespace QPlanAPI.DataAccess.Repositories
{


    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IRestaurantContext _context;
        private readonly IMapper _mapper;


        public RestaurantRepository(IRestaurantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Restaurant> GetRestaurant(string id)
        {
            return _mapper.Map<Restaurant>(await _context
                            .Restaurants
                            .FindAsync(r => r.Id.Equals(id)));
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            List<RestaurantEntity> response = await _context
                            .Restaurants
                            .FindAsync(_ => true)
                            .Result.ToListAsync();
            return _mapper.Map<List<Restaurant>>(response);
        }


        public async Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(Location location, double radius)
        {
            var result = await _context.Restaurants.Aggregate<RestaurantLocationEntity>(GetGeoNearQuery(location, radius)).ToListAsync();

            return _mapper.Map<List<Restaurant>>(result);
        }

        //TODO
        public async Task<bool> Create(Restaurant restaurant)
        {
            RestaurantEntity entity = _mapper.Map<RestaurantEntity>(restaurant);
            try
            {
                await _context.Restaurants.InsertOneAsync(entity);

            }
            catch
            {
                return false;
            }

            return true;
        }


        public async Task<bool> Delete(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> Update(Restaurant game)
        {
            throw new NotImplementedException();
        }

        #region "Private Methods"

        private List<BsonDocument> GetGeoNearQuery(Location location, double radius)
        {
            var geoNearOptions = new BsonDocument {
                { "near", new BsonDocument {
                    { "type", "Point" },
                    { "coordinates", new BsonArray {location.Longitude, location.Latitude} },
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
        #endregion
    }
}
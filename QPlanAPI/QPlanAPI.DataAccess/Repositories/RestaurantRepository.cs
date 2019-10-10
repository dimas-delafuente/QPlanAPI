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

        private const double EARTH_RADIUS_METERS = 6371000;

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
            var nearSphereFilter = new FilterDefinitionBuilder<RestaurantEntity>().NearSphere(r => r.Location, 
                new GeoJsonPoint<GeoJson2DCoordinates>(new GeoJson2DCoordinates(location.Longitude, location.Latitude)), radius);

            List<RestaurantEntity> restaurants = await _context.Restaurants.FindAsync(nearSphereFilter).Result.ToListAsync();
            restaurants.ForEach(r => {
                double distance = Math.Acos(Math.Sin(r.Location.Coordinates.Latitude * Math.PI / 180.0) * Math.Sin(location.Latitude * Math.PI / 180.0) +
                    Math.Cos(r.Location.Coordinates.Latitude * Math.PI / 180.0) * Math.Cos(location.Latitude * Math.PI / 180.0)
                    * Math.Cos((location.Longitude - r.Location.Coordinates.Longitude) * Math.PI / 180.0)) * EARTH_RADIUS_METERS;
            });
            return _mapper.Map<List<Restaurant>>(restaurants);
        }

        public async Task<bool> Insert(Restaurant restaurant)
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

        public async Task<bool> InsertMany(HashSet<Restaurant> restaurants)
        {
            List<RestaurantEntity> entities = _mapper.Map<List<RestaurantEntity>>(restaurants);
            try
            {
                await _context.Restaurants.InsertManyAsync(entities);
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

        public async Task<bool> DeleteByRestaurantType(RestaurantType type)
        {
            try
            {

                await _context.Restaurants.DeleteManyAsync(r => r.Type.Equals(type));
            }
            catch
            {
                return false;
            }

            return true;
        }


        public async Task<bool> Update(Restaurant restaurant)
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
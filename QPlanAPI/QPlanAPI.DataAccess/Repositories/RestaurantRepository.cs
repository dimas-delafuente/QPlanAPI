using System;
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

            return _mapper.Map<IEnumerable<Restaurant>>(await _context
                            .Restaurants
                            .FindAsync(_ => true)
                            .Result.ToListAsync());
        }

       
        public async Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(Location location, double radius)
        {
            var point = GeoJson.Point(GeoJson.Geographic(location.Longitude, location.Latitude));
            var filter = Builders<RestaurantEntity>.Filter.Near(r => r.Location, point, radius);

            return _mapper.Map<IEnumerable<Restaurant>>(await _context.Restaurants.FindAsync(filter).Result.ToListAsync());
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
    }
}

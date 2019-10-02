using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
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
            return _mapper.Map<Restaurant>(await _context.GetRestaurantById(id));
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return _mapper.Map<List<Restaurant>>(await _context.GetRestaurants());
        }


        public async Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(Location location, double radius)
        {
            return _mapper.Map<List<Restaurant>>(await _context.GetRestaurantsByLocation(location.Longitude, location.Latitude, radius));
        }

        public async Task<bool> Insert(Restaurant restaurant)
        {
            try
            {
                await _context.Insert(_mapper.Map<RestaurantEntity>(restaurant));

            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> InsertMany(HashSet<Restaurant> restaurants)
        {
            try
            {
                await _context.InsertMany(_mapper.Map<List<RestaurantEntity>>(restaurants));
            }
            catch
            {
                return false;
            }

            return true;
        }


        public async Task<bool> Delete(string id)
        {
            try
            {
                await _context.Delete(id);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteByRestaurantType(RestaurantType type)
        {
            try
            {
                await _context.DeleteByRestaurantType(type);
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

        
    }
}
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public abstract class BaseRestaurantsFeeder : IRestaurantsFeederService<FeedRestaurantsRequest>
    {

        private readonly IRestaurantRepository _restaurantRepository;


        protected BaseRestaurantsFeeder(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(FeedRestaurantsRequest request, Type responseType)
        {

            if (request.Endpoints.Any())
            {
                HashSet<Restaurant> restaurants = await GetRestaurants(request.Endpoints, responseType);

                try
                {
                    if (restaurants is object && restaurants.Any())
                    {
                        if (await _restaurantRepository.DeleteByRestaurantType(restaurants.FirstOrDefault().Type))
                        {
                            return await _restaurantRepository.InsertMany(restaurants);
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;

        }
        abstract public Task<HashSet<Restaurant>> GetRestaurants(List<string> endpoints, Type responseType);

    }

}

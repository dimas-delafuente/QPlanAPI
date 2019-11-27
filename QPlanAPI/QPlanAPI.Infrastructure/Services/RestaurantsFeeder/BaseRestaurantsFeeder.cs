using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public abstract class BaseRestaurantsFeeder : IRestaurantsFeederService<FeedRestaurantsRequest>
    {
        #region Properties
        public readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        #endregion Properties

        #region Ctor
        protected BaseRestaurantsFeeder(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        #endregion Ctor

        #region Public Methods

        public async Task<bool> Handle(FeedRestaurantsRequest request, Type responseType)
        {

            if (request.Endpoints?.Count > 0 || request is FeedLocalRestaurantsRequest)
            {
                HashSet<Restaurant> restaurants = await GetRestaurants(request, responseType);

                try
                {
                    if (restaurants is object && restaurants.Any() 
                        && await _restaurantRepository.DeleteByRestaurantType(restaurants.FirstOrDefault().Type))
                    {
                        return await _restaurantRepository.InsertMany(restaurants);
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        abstract public Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType);

        #endregion Public Methods
    }

}

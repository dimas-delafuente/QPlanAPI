using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class LocalRestaurantsFeeder : BaseRestaurantsFeeder
    {
        #region Ctor

        public LocalRestaurantsFeeder(IRestaurantRepository restaurantRepository, IMapper mapper) : base(mapper, restaurantRepository)
        {

        }

        #endregion Ctor

        #region Public Methods

        public override async Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType)
        {
            HashSet<Restaurant> localRestaurants = new HashSet<Restaurant>();

            var localRequest = request as FeedLocalRestaurantsRequest;

            var restaurantsRetrieved = await Task.Run( () => JsonConvert.DeserializeObject(localRequest.FileContent, responseType));
            Restaurant[] restaurants = _mapper.Map<Restaurant[]>(restaurantsRetrieved);

            localRestaurants.UnionWith(restaurants);

            return localRestaurants;
        }

        #endregion Public Methods
    }
}

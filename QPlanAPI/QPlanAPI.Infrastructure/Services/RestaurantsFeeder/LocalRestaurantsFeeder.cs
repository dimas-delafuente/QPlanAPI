using AutoMapper;
using Newtonsoft.Json;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class LocalRestaurantsFeeder : BaseRestaurantsFeeder
    {
        #region Consts
        private const string LOCAL_DIRECTORY = "";
        #endregion Consts

        #region Properties
        private readonly IMapper _mapper;
        #endregion Properties

        #region Ctor

        public LocalRestaurantsFeeder(IRestaurantRepository restaurantRepository, IMapper mapper) : base(restaurantRepository)
        {
            _mapper = mapper;
        }

        #endregion Ctor

        #region Public Methods

        public override async Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType)
        {
            HashSet<Restaurant> localRestaurants = new HashSet<Restaurant>();

            var localRequest = request as FeedLocalRestaurantsRequest;

            string responseContent = "";
            var restaurantsRetrieved = JsonConvert.DeserializeObject(responseContent, responseType);
            Restaurant[] restaurants = _mapper.Map<Restaurant[]>(restaurantsRetrieved);

            localRestaurants.UnionWith(restaurants);

            return localRestaurants;
        }

        #endregion Public Methods
    }
}

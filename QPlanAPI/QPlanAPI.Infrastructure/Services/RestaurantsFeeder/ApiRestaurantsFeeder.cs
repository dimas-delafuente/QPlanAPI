using System.Threading.Tasks;
using AutoMapper;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class ApiRestaurantsFeeder : IRestaurantsFeederService<FeedApiRestaurantsRequest>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private static HttpClient _client;

        public ApiRestaurantsFeeder(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _client = new HttpClient();

        }

        public async Task<bool> Handle(FeedApiRestaurantsRequest request, Type responseType)
        {

            if (request.ApiEndpoints.Any())
            {
                HashSet<Restaurant> apiRestaurants = await GetRestaurants(request.ApiEndpoints, responseType);

                try
                {
                    if (apiRestaurants is object && apiRestaurants.Any())
                    {
                        if (await _restaurantRepository.DeleteByRestaurantType(apiRestaurants.FirstOrDefault().Type))
                        {
                            return await _restaurantRepository.InsertMany(apiRestaurants);
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

        private async Task<HashSet<Restaurant>> GetRestaurants(List<string> ApiEndpoints, Type responseType)
        {
            //TODO No evita añadir repetidos
            HashSet<Restaurant> apiRestaurants = new HashSet<Restaurant>();

            foreach (string endpoint in ApiEndpoints)
            {
                try
                {
                    HttpResponseMessage endpointResponse = await _client.GetAsync(endpoint);

                    if (endpointResponse.IsSuccessStatusCode)
                    {
                        string responseContent = await endpointResponse.Content.ReadAsStringAsync();

                        if (!String.IsNullOrEmpty(responseContent))
                        {
                            var restaurantsRetrieved = JsonConvert.DeserializeObject(responseContent, responseType);
                            var restaurants = _mapper.Map<Restaurant[]>(restaurantsRetrieved);
                            apiRestaurants.UnionWith(restaurants);
                        }
                    }
                }
                catch (Exception e)
                {
                    return new HashSet<Restaurant>();
                }
            }

            return apiRestaurants;
        }
    }
}
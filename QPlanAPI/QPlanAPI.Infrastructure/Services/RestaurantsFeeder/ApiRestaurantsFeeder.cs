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
using System.Xml.Serialization;
using System.IO;

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
                HashSet<Restaurant> apiRestaurants = await GetRestaurants(request, responseType);

                try
                {
                    if (apiRestaurants is object && apiRestaurants.Any() 
                        && await _restaurantRepository.DeleteByRestaurantType(apiRestaurants.FirstOrDefault().Type))
                    {
                        return await _restaurantRepository.InsertMany(apiRestaurants);
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        #region Private Methods

        private async Task<HashSet<Restaurant>> GetRestaurants(FeedApiRestaurantsRequest request, Type responseType)
        {
            //TODO No evita añadir repetidos
            HashSet<Restaurant> apiRestaurants = new HashSet<Restaurant>();

            foreach (string endpoint in request.ApiEndpoints)
            {
                try
                {
                    HttpResponseMessage endpointResponse = await _client.GetAsync(endpoint);

                    if (endpointResponse.IsSuccessStatusCode)
                    {
                        string responseContent = await endpointResponse.Content.ReadAsStringAsync();

                        if (!String.IsNullOrEmpty(responseContent))
                        {
                            switch (request.ApiFormat)
                            {
                                case "XML":
                                    apiRestaurants.UnionWith(GetXmlRestaurants(responseContent, responseType));
                                    break;
                                case "HTML":
                                    // TO DO
                                    break;
                                default:
                                    apiRestaurants.UnionWith(GetJsonRestaurants(responseContent, responseType));
                                    break;
                            }
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

        private Restaurant[] GetJsonRestaurants(string responseContent, Type responseType)
        {
            var restaurantsRetrieved = JsonConvert.DeserializeObject(responseContent, responseType);
            return _mapper.Map<Restaurant[]>(restaurantsRetrieved);
        }

        private Restaurant[] GetXmlRestaurants(string responseContent, Type responseType)
        {
            var xmlSerializer = new XmlSerializer(responseType);
            object restaurantsRetrieved;

            using (TextReader reader = new StringReader(responseContent))
            {
                restaurantsRetrieved = xmlSerializer.Deserialize(reader);
            }

            return _mapper.Map<Restaurant[]>(restaurantsRetrieved);
        }

        #endregion Private Methods
    }
}
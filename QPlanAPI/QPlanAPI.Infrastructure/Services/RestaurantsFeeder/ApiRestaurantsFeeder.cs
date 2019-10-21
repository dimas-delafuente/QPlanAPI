using System.Threading.Tasks;
using AutoMapper;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class ApiRestaurantsFeeder : BaseRestaurantsFeeder
    { 
        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        public ApiRestaurantsFeeder(IMapper mapper, IRestaurantRepository restaurantRepository) : base(restaurantRepository)
        {
            _mapper = mapper;
            _client = new HttpClient();
        }

        public override async Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType)
        {
            //TODO No evita añadir repetidos
            HashSet<Restaurant> apiRestaurants = new HashSet<Restaurant>();

            var apiRequest = request as FeedApiRestaurantsRequest;

            foreach (string endpoint in apiRequest.Endpoints)
            {
                try
                {
                    HttpResponseMessage endpointResponse = await _client.GetAsync(endpoint);

                    if (endpointResponse.IsSuccessStatusCode)
                    {
                        string responseContent = await endpointResponse.Content.ReadAsStringAsync();

                        if (!String.IsNullOrEmpty(responseContent))
                        {
                            switch (apiRequest.ApiFormat)
                            {
                                case RestaurantFormat.XML:
                                    apiRestaurants.UnionWith(GetXmlRestaurants(responseContent, responseType));
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

        #region Private Methods

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
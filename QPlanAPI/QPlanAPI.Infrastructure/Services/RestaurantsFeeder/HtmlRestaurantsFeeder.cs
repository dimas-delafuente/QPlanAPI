﻿using AutoMapper;
using HtmlAgilityPack;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class HtmlRestaurantsFeeder : BaseRestaurantsFeeder
    {
        private const string USER_AGENT_VALID = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";
        private readonly IMapper _mapper;

        public HtmlRestaurantsFeeder(IRestaurantRepository restaurantRepository, IMapper mapper) : base(restaurantRepository)
        {
            _mapper = mapper;
        }

        public override async Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType)
        {
            HashSet<Restaurant> htmlRestaurants = new HashSet<Restaurant>();

            foreach (string endpoint in request.Endpoints)
            {
                try
                {
                    var doc = new HtmlDocument();
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endpoint);
                    webRequest.UserAgent = USER_AGENT_VALID;
                    var webResponse = await webRequest.GetResponseAsync();

                    doc.Load(webResponse.GetResponseStream());

                    var nodes = doc.DocumentNode.SelectNodes("//ul");
                    foreach (var node in nodes) 
                    {

                        var dominosPizzaRestaurant = new DominosPizzaRestaurantsResponse
                        {
                            Name = HttpUtility.HtmlDecode(node.SelectSingleNode("li/span[@itemprop='name']").InnerText),
                            Address = HttpUtility.HtmlDecode(node.SelectSingleNode("li/div/h3/span[@itemprop='streetAddress']").InnerText),
                            Url = node.SelectSingleNode("li/div/p/a[@class='nm']").Attributes["href"].Value,
                            Latitude = node.SelectSingleNode("li[@itemtype='http://schema.org/Restaurant']").Attributes["data-latitude"].Value,
                            Longitude = node.SelectSingleNode("li[@itemtype='http://schema.org/Restaurant']").Attributes["data-longitude"].Value,
                            Phone = node.SelectSingleNode("li/div/p/span[@itemprop='telephone']").InnerText,
                            Horario = node.SelectSingleNode("li/div/p/span/meta[@itemprop='openingHours']").Attributes["content"].Value
                        };

                        htmlRestaurants.Add(_mapper.Map<Restaurant>(dominosPizzaRestaurant));
                    }
                }
                catch (Exception e)
                {
                    return new HashSet<Restaurant>();
                }
            }

            return htmlRestaurants;
        }
    }
}

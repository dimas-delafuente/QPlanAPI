using AutoMapper;
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
        #region Consts

        private const string USER_AGENT_VALID = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";

        #endregion


        #region Properties

        private readonly IMapper _mapper;

        #endregion


        #region Ctor

        public HtmlRestaurantsFeeder(IRestaurantRepository restaurantRepository, IMapper mapper) : base(restaurantRepository)
        {
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public override async Task<HashSet<Restaurant>> GetRestaurants(FeedRestaurantsRequest request, Type responseType)
        {
            var htmlRestaurants = new HashSet<Restaurant>();

            foreach (string endpoint in request.Endpoints)
            {
                try
                {
                    var doc = new HtmlDocument();
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endpoint);
                    webRequest.UserAgent = USER_AGENT_VALID;
                    var webResponse = await webRequest.GetResponseAsync();

                    doc.Load(webResponse.GetResponseStream());

                    htmlRestaurants.UnionWith(_mapper.Map<Restaurant[]>(GetDominosPizzaRestaurants(doc)));

                }
                catch (Exception e)
                {
                    return new HashSet<Restaurant>();
                }
            }

            return htmlRestaurants;
        }

        #endregion

        #region Private Methods
        private HashSet<DominosPizzaRestaurantsResponse> GetDominosPizzaRestaurants(HtmlDocument doc)
        {
            var dominosPizzaRestaurants = new HashSet<DominosPizzaRestaurantsResponse>();
            try
            {
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//ul");
                foreach (var node in nodes)
                {
                    dominosPizzaRestaurants.Add(new DominosPizzaRestaurantsResponse
                    {
                        Name = HttpUtility.HtmlDecode(node.SelectSingleNode("li/span[@itemprop='name']").InnerText),
                        Address = HttpUtility.HtmlDecode(node.SelectSingleNode("li/div/h3/span[@itemprop='streetAddress']").InnerText),
                        Url = node.SelectSingleNode("li/div/p/a[@class='nm']").Attributes["href"].Value,
                        Latitude = node.SelectSingleNode("li[@itemtype='http://schema.org/Restaurant']").Attributes["data-latitude"].Value,
                        Longitude = node.SelectSingleNode("li[@itemtype='http://schema.org/Restaurant']").Attributes["data-longitude"].Value,
                        Phone = node.SelectSingleNode("li/div/p/span[@itemprop='telephone']").InnerText,
                        Schedule = node.SelectSingleNode("li/div/p/span/meta[@itemprop='openingHours']").Attributes["content"].Value
                    });
                }
            }
            catch (Exception e)
            {
                return new HashSet<DominosPizzaRestaurantsResponse>();
            }

            return dominosPizzaRestaurants;
        }


        #endregion
    }
}

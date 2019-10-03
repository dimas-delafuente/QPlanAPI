using AutoMapper;
using HtmlAgilityPack;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QPlanAPI.Infrastructure.Services.RestaurantsFeeder
{
    public class HtmlRestaurantsFeeder : BaseRestaurantsFeeder
    {

        private const string USER_AGENT_VALID = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public HtmlRestaurantsFeeder(IRestaurantRepository restaurantRepository, IMapper mapper) : base(restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public override async Task<HashSet<Restaurant>> GetRestaurants(List<string> endpoints, Type responseType)
        {
            HashSet<Restaurant> htmlRestaurants = new HashSet<Restaurant>();

            foreach (string endpoint in endpoints)
            {
                try
                {
                    var doc = new HtmlDocument();
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endpoint);
                    webRequest.UserAgent = USER_AGENT_VALID;
                    Stream stream = webRequest.GetResponse().GetResponseStream();
                    doc.Load(stream);

                    //RECORRER LOS NODOS


                    stream.Close();

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

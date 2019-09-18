using System.Collections.Generic;

namespace QPlanAPI.Config
{

    public enum RestaurantType
    {
        McDonalds,
        KFC,
        FostersHollywood,
        Ginos,
        TacoBell,
        PapaJohns,
        TGB
    }

    public class ExternalRestaurantsConfig
    {
        public List<ApiRestaurantsConfig> ApiRestaurants { get; set; }

        public class ApiRestaurantsConfig
        {
            public RestaurantType Type { get; set; }
            public List<string> Endpoints { get; set; }
        }
    }
}
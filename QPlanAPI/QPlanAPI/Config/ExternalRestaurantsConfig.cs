using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;

namespace QPlanAPI.Config
{
    public class ExternalRestaurantsConfig
    {
        public List<ApiRestaurantsConfig> ApiRestaurants { get; set; }

        public class ApiRestaurantsConfig
        {
            public RestaurantType Type { get; set; }
            public List<string> Endpoints { get; set; }
            public RestaurantFormat Format { get; set; }
        }
    }

    #region Enums

    public enum RestaurantType
    {
        McDonalds,
        KFC,
        FostersHollywood,
        Ginos,
        TacoBell,
        PapaJohns,
        TGB,
        Subway,
        DominosPizza
    }

    #endregion Enums
}
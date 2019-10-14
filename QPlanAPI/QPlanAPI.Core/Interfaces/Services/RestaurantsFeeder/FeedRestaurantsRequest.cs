using System.Collections.Generic;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{

    public class FeedRestaurantsRequest { }

    public class FeedApiRestaurantsRequest : FeedRestaurantsRequest
    {
        public List<string> ApiEndpoints { get; set; }

        public RestaurantFormat ApiFormat { get; set; }
    }

    #region Enums

    public enum RestaurantFormat
    {
        JSON,
        XML,
        HTML
    }

    #endregion Enums
}
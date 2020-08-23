using System.Collections.Generic;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{

    public class FeedRestaurantsRequest {

        public List<string> Endpoints { get; set; }
    }

    public class FeedApiRestaurantsRequest : FeedRestaurantsRequest
    {
        public RestaurantFormat ApiFormat { get; set; }
    }

    public class FeedHtmlRestaurantsRequest : FeedRestaurantsRequest
    {

    }

    public class FeedLocalRestaurantsRequest : FeedRestaurantsRequest
    {
        public string FileContent { get; set; }
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
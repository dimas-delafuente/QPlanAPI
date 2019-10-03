using System.Collections.Generic;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{

    public class FeedRestaurantsRequest {

        public List<string> Endpoints { get; set; }

    }


    public class FeedApiRestaurantsRequest : FeedRestaurantsRequest
    {
    }

    public class FeedHtmlRestaurantsRequest : FeedRestaurantsRequest
    {
    }
}
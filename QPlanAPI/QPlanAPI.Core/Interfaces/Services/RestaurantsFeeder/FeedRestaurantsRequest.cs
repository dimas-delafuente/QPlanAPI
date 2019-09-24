using System.Collections.Generic;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{

    public class FeedRestaurantsRequest { }

    public class FeedApiRestaurantsRequest : FeedRestaurantsRequest
    {
        public List<string> ApiEndpoints { get; set; }
    }
}
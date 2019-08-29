using System.Collections.Generic;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class GetRestaurantsResponse : UseCaseResponse
    {
        public List<Restaurant> Restaurants { get; set; }

        public GetRestaurantsResponse(List<Restaurant> restaurants, bool success, string message) : base(success, message)
        {
            Restaurants = restaurants;
        }

    }
}

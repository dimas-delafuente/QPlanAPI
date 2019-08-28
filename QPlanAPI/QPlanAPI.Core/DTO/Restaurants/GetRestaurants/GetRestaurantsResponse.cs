using System.Collections.Generic;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class GetRestaurantsResponse : UseCaseResponse
    {
        public GetRestaurantsResponse(bool success, string message) : base(success, message)
        {
        }

        public List<Restaurant> Restaurants { get; set; }
    }
}

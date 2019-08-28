using System.Collections.Generic;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class AddRestaurantResponse : UseCaseResponse
    {

        public string Id { get; }


        public AddRestaurantResponse(bool success, string message) : base(success, message)
        {
        }

        public AddRestaurantResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}

using System;
namespace QPlanAPI.Core.DTO.Restaurants
{
    public class AddRestaurantResponse : UseCaseResponse
    {
        public AddRestaurantResponse(bool success, string message) : base(success, message)
        {
        }
    }
}

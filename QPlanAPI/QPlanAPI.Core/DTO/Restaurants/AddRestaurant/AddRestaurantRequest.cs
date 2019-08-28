using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class AddRestaurantRequest : Restaurant, IUseCaseRequest<AddRestaurantResponse>
    {

    }
}

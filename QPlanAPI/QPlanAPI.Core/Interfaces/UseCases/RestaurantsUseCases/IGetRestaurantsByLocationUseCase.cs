using System;
using QPlanAPI.Core.DTO.Restaurants;

namespace QPlanAPI.Core.Interfaces.UseCases
{
    public interface IGetRestaurantsByLocationUseCase : IUseCaseRequestHandler<GetRestaurantsByLocationRequest, GetRestaurantsResponse>
    {
    }
}

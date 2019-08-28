using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.UseCases.Restaurants;

namespace QPlanAPI.Core.Interfaces.UseCases
{
    public interface IGetAllRestaurantsUseCase : IUseCaseRequestHandler<GetRestaurantsRequest, GetRestaurantsResponse>
    {
    }
}

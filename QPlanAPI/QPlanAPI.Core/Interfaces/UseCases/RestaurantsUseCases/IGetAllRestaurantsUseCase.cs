using QPlanAPI.Core.DTO.Restaurants;
namespace QPlanAPI.Core.Interfaces.UseCases
{
    public interface IGetAllRestaurantsUseCase : IUseCaseRequestHandler<GetRestaurantsRequest, GetRestaurantsResponse>
    {
    }
}

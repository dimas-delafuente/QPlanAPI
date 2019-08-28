using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using System.Linq;

namespace QPlanAPI.Core.UseCases.RestaurantsUseCases
{
    public class GetAllRestaurantsUseCase : IGetAllRestaurantsUseCase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public GetAllRestaurantsUseCase(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(GetRestaurantsRequest request, IOutputPort<GetRestaurantsResponse> outputPort)
        {
            var response = await _restaurantRepository.GetAllRestaurants();
            outputPort.Handle(response.Any() ? new GetRestaurantsResponse(true, string.Empty) : new GetRestaurantsResponse(false, string.Empty));
            return response.Any();
        }
    }
}

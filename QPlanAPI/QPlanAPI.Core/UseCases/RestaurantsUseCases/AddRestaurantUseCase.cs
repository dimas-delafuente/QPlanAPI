using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Core.Interfaces.UseCases.Restaurants;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.UseCases.Restaurants
{
    public class AddRestaurantUseCase : IAddRestaurantUseCase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public AddRestaurantUseCase(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(AddRestaurantRequest message, IOutputPort<AddRestaurantResponse> outputPort)
        {
            var response = await _restaurantRepository.Create(new Restaurant());
            outputPort.Handle(response.Success ? new AddRestaurantResponse(true, string.Empty) : new AddRestaurantResponse(false, string.Empty));
            return response.Success;
        }
    }
}

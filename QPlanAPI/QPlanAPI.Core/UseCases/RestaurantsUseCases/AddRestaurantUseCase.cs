using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.UseCases
{
    public class AddRestaurantUseCase : IAddRestaurantUseCase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public AddRestaurantUseCase(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(AddRestaurantRequest request, IOutputPort<AddRestaurantResponse> outputPort)
        {
            bool created = await _restaurantRepository.Create(new Restaurant
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Location = request.Location,
                Phone = request.Phone,
                Categories = request.Categories,
                Rating = request.Rating,
                Url = request.Url,
                CoverUrl = request.Url
            });
            outputPort.Handle(created ? new AddRestaurantResponse(true, string.Empty) : new AddRestaurantResponse(false, string.Empty));
            return created;
        }
    }
}

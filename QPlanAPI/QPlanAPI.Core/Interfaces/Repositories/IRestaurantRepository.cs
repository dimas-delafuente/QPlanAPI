using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {
        Task<AddRestaurantResponse> Create(Restaurant restaurant);

    }
}

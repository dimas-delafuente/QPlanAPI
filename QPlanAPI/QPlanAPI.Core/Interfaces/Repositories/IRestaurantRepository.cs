using System.Collections.Generic;
using System.Threading.Tasks;
using QPlanAPI.Domain;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {

        Task<Restaurant> GetRestaurant(string name);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(Location location, double radius);

        Task<bool> Create(Restaurant restaurant);
        Task<bool> Update(Restaurant game);
        Task<bool> Delete(string name);


    }
}

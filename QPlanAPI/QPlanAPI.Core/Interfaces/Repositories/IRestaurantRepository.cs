using System.Collections.Generic;
using System.Threading.Tasks;
using QPlanAPI.Domain;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.Interfaces.Repositories
{
    public interface IRestaurantRepository
    {

        Task<Restaurant> GetRestaurant(string name);
        Task<IEnumerable<Restaurant>> GetPagedRestaurants(int page, int pageSize);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<IEnumerable<Restaurant>> GetRestaurantsByLocation(Location location, double radius);

        Task<bool> Insert(Restaurant restaurant);
        Task<bool> InsertMany(HashSet<Restaurant> restaurant);
        Task<bool> Update(Restaurant restaurant);
        Task<bool> Delete(string name);
        Task<bool> DeleteByRestaurantType(RestaurantType type);


    }
}

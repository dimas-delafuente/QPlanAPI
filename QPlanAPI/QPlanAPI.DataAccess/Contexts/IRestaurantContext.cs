using QPlanAPI.DataAccess.Entities;
using QPlanAPI.Domain.Restaurants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QPlanAPI.DataAccess.Contexts
{
    public interface IRestaurantContext
    {
        Task<RestaurantEntity> GetRestaurantById(string id);

        Task<List<RestaurantEntity>> GetRestaurants();
        Task<List<RestaurantLocationEntity>> GetRestaurantsByLocation(double longitude, double latitude, double radius);

        Task Insert(RestaurantEntity restaurant);
        Task InsertMany(List<RestaurantEntity> restaurant);
        Task Update(RestaurantEntity restaurant);
        Task Delete(string id);
        Task DeleteByRestaurantType(RestaurantType type);


    }
}

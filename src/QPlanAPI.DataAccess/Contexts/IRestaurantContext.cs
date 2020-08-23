using MongoDB.Driver;
using QPlanAPI.DataAccess.Entities;

namespace QPlanAPI.DataAccess.Contexts
{
    public interface IRestaurantContext
    {

        IMongoCollection<RestaurantEntity> Restaurants { get; }
    }
}

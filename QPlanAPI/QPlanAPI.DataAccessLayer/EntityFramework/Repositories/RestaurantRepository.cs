using System;
using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.DataAccess.EntityFramework.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        /* Aquí meter AutoMapper */

        public RestaurantRepository()
        {
        }

        public Task<AddRestaurantResponse> Create(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}

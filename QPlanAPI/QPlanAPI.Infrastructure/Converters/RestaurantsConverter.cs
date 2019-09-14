using System.Collections.Generic;
using AutoMapper;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Infrastructure.Converters
{
    public class KFCResponseToRestaurantConverter : ITypeConverter<KFCRestaurantResponse, Restaurant[]>
    {
        public Restaurant[] Convert(KFCRestaurantResponse src, Restaurant[] dest, ResolutionContext context)
        {
            List<Restaurant> restaurants = new List<Restaurant> { };
            foreach (var restaurant in src.Restaurants)
            {
                restaurants.Add(context.Mapper.Map<Restaurant>(restaurant));
            }

            return restaurants.ToArray();
        }

    }
}
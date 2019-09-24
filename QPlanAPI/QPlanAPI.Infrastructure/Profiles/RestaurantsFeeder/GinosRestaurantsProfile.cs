using AutoMapper;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Infrastructure.Converters;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class GinosRestaurantsProfile : Profile
    {
        public GinosRestaurantsProfile()
        {
            CreateMap<GinosRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new GinosResponseToRestaurantConverter());
        }
    }
}

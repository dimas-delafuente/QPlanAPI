using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Infrastructure.Converters;
using System.Text.RegularExpressions;
using System.Linq;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class KFCRestaurantsProfile : BaseProfile
    {
        #region Public Methods

        public KFCRestaurantsProfile()
        {
            CreateMap<KFCRestaurantsResponse.KFCRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.KFC))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => 
                    GetLocation(src.Geometry.Coordinates.ElementAtOrDefault(0), src.Geometry.Coordinates.ElementAtOrDefault(1))))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Properties.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Properties.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Properties.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Properties.PostalCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => Regex.Replace(src.Properties.Phone, PhoneRegex, string.Empty).Trim()));

            CreateMap<KFCRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new KFCResponseToRestaurantConverter());
        }

        #endregion Public Methods
    }
}

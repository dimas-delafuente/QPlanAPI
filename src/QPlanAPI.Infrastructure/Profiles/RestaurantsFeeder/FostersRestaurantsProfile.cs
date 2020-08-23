using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class FostersRestaurantsProfile : BaseProfile
    {
        #region Public Methods

        public FostersRestaurantsProfile()
        {
            CreateMap<FostersRestaurantsResponse, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.FostersHollywood))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Location.Longitude, src.Location.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Location.PostalCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => Regex.Replace(src.Phone, PhoneRegex, string.Empty).Trim()))
                .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverUrl));
        }

        #endregion Public Methods
    }
}

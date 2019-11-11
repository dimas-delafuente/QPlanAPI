using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Infrastructure.Converters;
using System.Text.RegularExpressions;
using System.Linq;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class TGBRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string TGBAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";
        #endregion Constants

        #region Public Methods

        public TGBRestaurantsProfile()
        {
            CreateMap<TGBRestaurantsResponse.TGBInformation.TGBRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.TGB))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => 
                    GetLocation(src.Coordinates.Split(',').ElementAtOrDefault(0), src.Coordinates.Split(',').ElementAtOrDefault(1))))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetTGBAddressField(src.Address)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

            CreateMap<TGBRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new TGBResponseToRestaurantConverter());
        }

        #endregion Public Methods

        #region Private Methods

        private string GetTGBAddressField(string address)
        {
            if (!Regex.IsMatch(address, PostalCodeRegex))
                return address;

            Regex r = new Regex(TGBAddressRegex);
            string parsedAddress = r.Match(address)?.Value.Trim();

            return !string.IsNullOrEmpty(parsedAddress) && parsedAddress.EndsWith(",") ? parsedAddress.Remove(parsedAddress.Length - 1) : parsedAddress;
        }

        #endregion Private Methods
    }
}

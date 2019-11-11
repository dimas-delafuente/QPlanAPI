using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Infrastructure.Converters;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class SubwayRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string SubwayAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";
        private const string SubwayName = "Subway";
        #endregion Constants

        #region Public Methods

        public SubwayRestaurantsProfile()
        {
            CreateMap<SubwayRestaurantsResponse.SubwayRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.Subway))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{SubwayName} {src.Name}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetSubwayAddressField(src.Address)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<SubwayRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new SubwayResponseToRestaurantConverter());
        }

        #endregion Public Methods

        #region Private Methods

        private string GetSubwayAddressField(string address)
        {
            if (!Regex.IsMatch(address, PostalCodeRegex))
                return address;

            Regex r = new Regex(SubwayAddressRegex);
            string parsedAddress = r.Match(address)?.Value.Trim();

            if (!string.IsNullOrEmpty(parsedAddress))
            {
                if (parsedAddress.EndsWith(","))
                    parsedAddress = parsedAddress.Remove(parsedAddress.Length - 1);
                else if (parsedAddress.EndsWith("-"))
                    parsedAddress = parsedAddress.Remove(parsedAddress.Length - 2);
            }

            return parsedAddress;
        }

        #endregion Private Methods
    }
}

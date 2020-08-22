using System.Collections.Generic;
using System.Text.RegularExpressions;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class LaSurenaRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string LaSurenaAddressSeparator = " C.P.";
        private const string LaSurenaName = "La Sureña";
        #endregion Constants

        #region Public Methods

        public LaSurenaRestaurantsProfile()
        {
            CreateMap<LaSurenaRestaurantsResponse, Restaurant>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.LaSurena))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{LaSurenaName} {src.Address}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetLaSurenaAddress(src.Address)));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetLaSurenaAddress(string address)
        {
            if (!Regex.IsMatch(address, PostalCodeRegex))
                return address;

            return Regex.Split(address, LaSurenaAddressSeparator)?[0];
        }

        #endregion Private Methods
    }
}

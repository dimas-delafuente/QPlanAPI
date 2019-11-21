using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class LaSureñaRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string LaSureñaAddressSeparator = " C.P.";
        private const string LaSureñaName = "La Sureña";
        #endregion Constants

        #region Public Methods

        public LaSureñaRestaurantsProfile()
        {
            CreateMap<LaSureñaRestaurantsResponse, Restaurant>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.Montaditos))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{LaSureñaName} {src.Address}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetLaSureñaAddress(src.Address)));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetLaSureñaAddress(string address)
        {
            if (!Regex.IsMatch(address, PostalCodeRegex))
                return address;

            return Regex.Split(address, LaSureñaAddressSeparator)?[0];
        }

        #endregion Private Methods
    }
}

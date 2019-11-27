using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class MontaditosRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string MontaditosAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";
        private const string MontaditosName = "100 Montaditos";
        #endregion Constants

        #region Public Methods

        public MontaditosRestaurantsProfile()
        {
            CreateMap<MontaditosRestaurantsResponse, Restaurant>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.Montaditos))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{MontaditosName} {src.Address}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetMontaditosAddress(src.Address)));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetMontaditosAddress(string address)
        {
            if (!Regex.IsMatch(address, PostalCodeRegex))
                return address;

            Regex r = new Regex(MontaditosAddressRegex);
            string parsedAddress = r.Match(address)?.Value.Trim();

            return !string.IsNullOrEmpty(parsedAddress) && parsedAddress.EndsWith(",") ? parsedAddress.Remove(parsedAddress.Length - 1) : parsedAddress;
        }

        #endregion Private Methods
    }
}

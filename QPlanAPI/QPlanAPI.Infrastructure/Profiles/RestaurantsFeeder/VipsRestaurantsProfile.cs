using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class VipsRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string VipsAddressSeparator = "VIPS ";
        #endregion Constants

        #region Public Methods

        public VipsRestaurantsProfile()
        {
            CreateMap<VipsRestaurantsResponse, Restaurant>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.Vips))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetVipsAddress(src.Name)));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetVipsAddress(string name)
        {
            var address = Regex.Split(name, VipsAddressSeparator);
            return address != null && address.Length > 1 ? address[1] : string.Empty;
        }

        #endregion Private Methods
    }
}

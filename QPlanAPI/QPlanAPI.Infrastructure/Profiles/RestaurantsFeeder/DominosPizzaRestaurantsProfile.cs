using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain.Restaurants;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
   public class DominosPizzaRestaurantsProfile : BaseProfile
    {
        #region Constants
        private const string DominosPizzaCityRegex = @"((?<=\d{4,5}[\s.,])([^\,^\.^\-^\(])+)";
        #endregion Constants

        #region Public Methods

        public DominosPizzaRestaurantsProfile()
        {
            CreateMap<DominosPizzaRestaurantsResponse, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.DominosPizza))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { FastFoodCategory }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => GetCity(src.Address).ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetCity(string address)
        {
            Regex r = new Regex(DominosPizzaCityRegex);
            return r.Match(address)?.Value.Trim().ToUpper();
        }

        #endregion Private Methods
    }
}

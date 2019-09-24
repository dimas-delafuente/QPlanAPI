using AutoMapper;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Domain;
using System;
using QPlanAPI.Infrastructure.Converters;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class FostersRestaurantsProfile : Profile
    {
        public FostersRestaurantsProfile()
        {
            CreateMap<FostersRestaurantsResponse, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.FostersHollywood))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetFostersLocation(src.Location.Longitude, src.Location.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Location.PostalCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => Regex.Replace(src.Phone, @"[^\d]", "").Trim()))
                .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverUrl));
        }

        private Location GetFostersLocation(string longitude, string latitude)
        {
            try
            {
                double lng = Convert.ToDouble(longitude);
                double lat = Convert.ToDouble(latitude);
                return new Location(lng, lat);
            }
            catch
            {
                return new Location(0, 0);
            }
        }
    }
}

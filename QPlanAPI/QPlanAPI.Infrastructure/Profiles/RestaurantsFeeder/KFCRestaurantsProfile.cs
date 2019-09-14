using AutoMapper;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Domain;
using System;
using QPlanAPI.Infrastructure.Converters;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class KFCRestaurantsProfile : Profile
    {
        public KFCRestaurantsProfile()
        {
            CreateMap<KFCRestaurantResponse.KFCRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.KFC))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetKFCLocation(src.Geometry.Coordinates)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Properties.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Properties.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Properties.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Properties.PostalCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Properties.Phone));

            CreateMap<KFCRestaurantResponse, Restaurant[]>()
            .ConvertUsing(new KFCResponseToRestaurantConverter());
        }

        private Location GetKFCLocation(string[] coordinates)
        {
            try
            {
                double lng = Convert.ToDouble(coordinates[0]);
                double lat = Convert.ToDouble(coordinates[1]);
                return new Location(lng, lat);
            }
            catch
            {
                return new Location(0, 0);
            }

        }


    }
}

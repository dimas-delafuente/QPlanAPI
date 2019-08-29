using System;
using AutoMapper;
using QPlanAPI.DataAccess.Entities;
using QPlanAPI.Domain.Restaurants;
using MongoDB.Driver.GeoJsonObjectModel;
using QPlanAPI.Domain;
using System.Collections.Generic;

namespace QPlanAPI.DataAccess.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantEntity>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => 
                new GeoJsonPoint<GeoJson2DGeographicCoordinates>(GeoJson.Geographic(src.Location.Longitude, src.Location.Latitude))));

            CreateMap<RestaurantEntity, Restaurant>().
                ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                    new Location(src.Location.Coordinates.Longitude, src.Location.Coordinates.Latitude)))
                    .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        }
    }
}

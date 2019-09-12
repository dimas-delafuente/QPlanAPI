using AutoMapper;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Domain;
using System;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class FeedRestaurantsProfile : Profile
    {
        public FeedRestaurantsProfile()
        {
            CreateMap<McDonaldsRestaurantsResponse, Restaurant>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.McDonalds))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetMcDonaldsLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => GetMcDonaldsCity(src.Address)))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetMcDonaldsPostalCode(src.Address)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetMcDonaldsAddress(src.Address)));
        }

        private Location GetMcDonaldsLocation(string longitude, string latitude)
        {
            try
            {
                double lng = Convert.ToDouble(longitude.Split(',')[0]);
                double lat = Convert.ToDouble(latitude.Split(',')[0]);
                return new Location(lng, lat);
            }
            catch
            {
                return new Location(0, 0);
            }

        }
        private string GetMcDonaldsCity(string address)
        {
            var addressFields = address.Split(',');
            return addressFields[addressFields.Length - 2].Trim();
        }

        private string GetMcDonaldsPostalCode(string address)
        {
            try
            {
                var addressFields = address.Split(',');
                return addressFields[addressFields.Length - 1].Trim();
            }
            catch
            {
                return string.Empty;
            }

        }

        //TODO 
        private string GetMcDonaldsAddress(string address)
        {
            var addressFields = address.Split(',');
            var baseAddress = string.Empty;

            for (int i = 0; i < addressFields.Length - 2; i++)
                baseAddress += addressFields[i];

            return baseAddress.Trim();
        }
    }
}

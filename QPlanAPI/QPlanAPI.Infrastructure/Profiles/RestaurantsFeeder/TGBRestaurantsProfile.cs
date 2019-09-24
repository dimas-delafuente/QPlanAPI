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
    public class TGBRestaurantsProfile : Profile
    {
        private const string TGBAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";
        private const string TGBPostalCodeRegex = @"\d{4,5}";

        public TGBRestaurantsProfile()
        {
            CreateMap<TGBRestaurantsResponse.TGBInformation.TGBRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.TGB))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetTGBLocation(src.Coordinates)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetTGBAddressField(src.Address)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetTGBPostalCode(src.Address)))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

            CreateMap<TGBRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new TGBResponseToRestaurantConverter());
        }

        private Location GetTGBLocation(string latlong)
        {
            try
            {
                string[] coordinates = latlong.Split(',');
                double lng = Convert.ToDouble(coordinates[1].Trim());
                double lat = Convert.ToDouble(coordinates[0].Trim());
                return new Location(lng, lat);
            }
            catch
            {
                return new Location(0, 0);
            }

        }

        private string GetTGBPostalCode(string address)
        {
            Regex r = new Regex(TGBPostalCodeRegex);
            string postalCode = r.Match(address)?.Value.Trim();
            return postalCode.Length < 5 ? $"0{postalCode}" : postalCode;
        }

        private string GetTGBAddressField(string address)
        {
            if (!Regex.IsMatch(address, TGBPostalCodeRegex))
                return address;

            Regex r = new Regex(TGBAddressRegex);
            string parsedAddress = r.Match(address)?.Value.Trim();

            return !string.IsNullOrEmpty(parsedAddress) && parsedAddress.EndsWith(",") ? parsedAddress.Remove(parsedAddress.Length - 1) : parsedAddress;
        }
    }
}

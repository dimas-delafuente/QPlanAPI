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
    public class PapaJohnsRestaurantsProfile : BaseProfile
    {
        private const string PapaJohnsAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";
        private const string PapaJohnsPostalCodeRegex = @"\d{4,5}";

        public PapaJohnsRestaurantsProfile()
        {
            CreateMap<PapaJohnsRestaurantsResponse.PapaJohnsRestaurant, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.PapaJohns))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetPapaJohnsAddressField(src.Address)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.ToUpper()))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));

            CreateMap<PapaJohnsRestaurantsResponse, Restaurant[]>()
            .ConvertUsing(new PapaJohnsResponseToRestaurantConverter());
        }


        private string GetPapaJohnsAddressField(string address)
        {
            if (!Regex.IsMatch(address, PapaJohnsPostalCodeRegex))
                return address;

            Regex r = new Regex(PapaJohnsAddressRegex);
            string parsedAddress = r.Match(address)?.Value.Trim();

            return !string.IsNullOrEmpty(parsedAddress) && parsedAddress.EndsWith(",") ? parsedAddress.Remove(parsedAddress.Length - 1) : parsedAddress;
        }
    }
}

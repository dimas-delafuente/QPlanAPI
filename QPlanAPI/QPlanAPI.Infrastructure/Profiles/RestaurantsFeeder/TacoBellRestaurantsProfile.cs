using AutoMapper;
using QPlanAPI.Domain.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using System.Collections.Generic;
using QPlanAPI.Domain;
using System;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public class TacoBellRestaurantsProfile : BaseProfile
    {
        private const string TacoBellNameRegex = @"(C\.C\.)([^\,^\.^\-^\)])+";
        private const string TacoBellPostalCodeRegex = @"\d{4,5}";

        private const string TacoBellCityRegex = @"((?<=\d{4,5}[\s.,])([^\,^\.^\-^\(])+)";

        private const string TacoBellAddressRegex = @"([^\,^\.^\-^\)]).+(?=([^\,\.\-\)]\d{4,5}))";

        public TacoBellRestaurantsProfile()
        {
            CreateMap<TacoBellRestaurantsResponse, Restaurant>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => RestaurantType.TacoBell))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(_ => new List<string> { "FastFood" }))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => GetLocation(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetTacoBellName(src.Address)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => GetTacoBellAddressField(src.Address)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => GetTacoBellCity(src.Address)))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => GetPostalCode(src.Address)))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
        }

        private string GetTacoBellName(string address)
        {
            string pattern = address.Contains("C.C") ? TacoBellNameRegex : TacoBellAddressRegex;
            Regex r = new Regex(pattern);

            return r.Match(address)?.Value.Trim();
        }

        private string GetTacoBellAddressField(string address)
        {
            string parsedAddress = address;
            if (address.Contains("C.C"))
                parsedAddress = Regex.Replace(parsedAddress, TacoBellNameRegex, string.Empty);

            Regex r = new Regex(TacoBellAddressRegex);

            return r.Match(parsedAddress)?.Value.Trim();
        }

        private string GetTacoBellCity(string address)
        {
            Regex r = new Regex(TacoBellCityRegex);

            return r.Match(address)?.Value.Trim().ToUpper();
        }
    }
}

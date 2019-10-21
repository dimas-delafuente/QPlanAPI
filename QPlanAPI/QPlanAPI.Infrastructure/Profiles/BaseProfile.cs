using AutoMapper;
using QPlanAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QPlanAPI.Infrastructure.Profiles
{
    public abstract class BaseProfile : Profile
    {

        private const string PostalCodeRegex = @"\d{4,5}";

        public Location GetLocation(string longitude, string latitude)
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

        public Location GetLocation(string[] coordinates)
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

        public string GetPostalCode(string address)
        {
            Regex r = new Regex(PostalCodeRegex);
            string postalCode = r.Match(address)?.Value.Trim();
            return postalCode.Length < 5 ? $"0{postalCode}" : postalCode;
        }
    }
}

using AutoMapper;
using QPlanAPI.Domain;
using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace QPlanAPI.Infrastructure.Profiles
{
    public abstract class BaseProfile : Profile
    {
        #region Constants
        public const string PhoneRegex = @"[^\d]";
        public const string PostalCodeRegex = @"\d{4,5}";
        public const string FastFoodCategory = "FastFood";
        #endregion Constants

        #region Public Methods

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

        public Location GetLocation(string location)
        {
            string[] coordinates = location.Split(',');
            return GetLocation(coordinates.ElementAtOrDefault(0), coordinates.ElementAtOrDefault(1));
        }

        public string GetPostalCode(string address)
        {
            Regex r = new Regex(PostalCodeRegex);
            string postalCode = r.Match(address)?.Value.Trim();
            return postalCode?.Length < 5 ? $"0{postalCode}" : postalCode;
        }

        #endregion Public Methods
    }
}

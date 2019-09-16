using System.Collections.Generic;
using AutoMapper;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Domain;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Infrastructure.Converters
{
    public class KFCResponseToRestaurantConverter : ITypeConverter<KFCRestaurantsResponse, Restaurant[]>
    {
        public Restaurant[] Convert(KFCRestaurantsResponse src, Restaurant[] dest, ResolutionContext context)
        {
            List<Restaurant> restaurants = new List<Restaurant> { };
            foreach (var restaurant in src.Restaurants)
            {
                restaurants.Add(context.Mapper.Map<Restaurant>(restaurant));
            }

            return restaurants.ToArray();
        }

    }

    public class GinosResponseToRestaurantConverter : ITypeConverter<GinosRestaurantsResponse, Restaurant[]>
    {
        public Restaurant[] Convert(GinosRestaurantsResponse src, Restaurant[] dest, ResolutionContext context)
        {
            List<Restaurant> restaurants = new List<Restaurant> { };
            foreach (var restaurant in src.Restaurants)
            {
                string[] cityFields = restaurant[9].Split('-');
                try
                {
                    var convertedRestaurant = new Restaurant
                    {
                        Name = restaurant[1],
                        Location = GetGinosLocation(restaurant[3], restaurant[2]),
                        Address = restaurant[4],
                        Url = restaurant[6],
                        CoverUrl = restaurant[7],
                        Phone = restaurant[8],
                        City = cityFields[0].ToUpper(),
                        PostalCode = cityFields[1],
                        Type = RestaurantType.Ginos,
                        Categories = new List<string> { "FastFood" }
                    };

                    restaurants.Add(convertedRestaurant);
                }
                catch
                {
                    //TODO
                }
            }

            return restaurants.ToArray();
        }

        private Location GetGinosLocation(string longitue, string latitude)
        {
            try
            {
                double lng = System.Convert.ToDouble(longitue);
                double lat = System.Convert.ToDouble(latitude);
                return new Location(lng, lat);
            }
            catch
            {
                return new Location(0, 0);
            }

        }

    }


    public class PapaJohnsResponseToRestaurantConverter : ITypeConverter<PapaJohnsRestaurantsResponse, Restaurant[]>
    {
        public Restaurant[] Convert(PapaJohnsRestaurantsResponse src, Restaurant[] dest, ResolutionContext context)
        {
            List<Restaurant> restaurants = new List<Restaurant> { };
            foreach (var restaurant in src.Restaurants)
            {
                restaurants.Add(context.Mapper.Map<Restaurant>(restaurant));
            }

            return restaurants.ToArray();
        }

    }
}
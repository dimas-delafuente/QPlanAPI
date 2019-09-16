using System.Collections.Generic;
using Newtonsoft.Json;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{
    public class FeedRestaurantsResponse { }


    public class McDonaldsRestaurantsResponse : FeedRestaurantsResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lng")]
        public string Longitude { get; set; }

        [JsonProperty("telefono")]
        public string Phone { get; set; }

        [JsonProperty("idUrl")]
        public string Url { get; set; }

        [JsonProperty("horario")]
        public string Horario { get; set; }

    }
    public class KFCRestaurantsResponse : FeedRestaurantsResponse
    {
        [JsonProperty("features")]
        public KFCRestaurant[] Restaurants { get; set; }

        public class KFCRestaurant
        {
            public KFCGeometry Geometry { get; set; }
            public KFCProperties Properties { get; set; }

            public class KFCProperties
            {
                [JsonProperty("title")]
                public string Name { get; set; }
                public string Address { get; set; }

                [JsonProperty("postal")]
                public string PostalCode { get; set; }

                [JsonProperty("locality")]
                public string City { get; set; }
                public string Phone { get; set; }


            }

            public class KFCGeometry
            {
                public string[] Coordinates { get; set; }
            }
        }


    }

    public class FostersRestaurantsResponse : FeedRestaurantsResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Url { get; set; }

        [JsonProperty("image")]
        public string CoverUrl { get; set; }

        [JsonProperty("location")]
        public ForstersLocation Location { get; set; }

        public class ForstersLocation
        {
            [JsonProperty("lat")]
            public string Latitude { get; set; }
            [JsonProperty("lon")]
            public string Longitude { get; set; }
            [JsonProperty("postal_code")]
            public string PostalCode { get; set; }
            [JsonProperty("thoroughfare")]
            public string Address { get; set; }
            [JsonProperty("locality")]
            public string City { get; set; }
        }
    }

    public class GinosRestaurantsResponse : FeedRestaurantsResponse
    {
        [JsonProperty("markers")]
        public List<List<string>> Restaurants { get; set; }
    }

    public class TacoBellRestaurantsResponse : FeedRestaurantsResponse
    {
        [JsonProperty("direccion")]
        public string Address { get; set; }

        [JsonProperty("telefono")]
        public string Phone { get; set; }

        [JsonProperty("justeat_web")]
        public string Url { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class PapaJohnsRestaurantsResponse : FeedRestaurantsResponse
    {
        [JsonProperty("page")]
        public List<PapaJohnsRestaurant> Restaurants { get; set; }

        public class PapaJohnsRestaurant
        {
            public string Name { get; set; }

            [JsonProperty("text_address")]
            public string Address { get; set; }

            [JsonProperty("commune")]
            public string City { get; set; }

            public string Phone { get; set; }

            public string Latitude { get; set; }

            public string Longitude { get; set; }
        }
    }
}
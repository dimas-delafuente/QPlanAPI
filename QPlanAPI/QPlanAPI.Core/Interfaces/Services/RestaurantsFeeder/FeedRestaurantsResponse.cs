using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{
    public class FeedRestaurantsResponse { }


    [Serializable]
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


    public class KFCRestaurantResponse : FeedRestaurantsResponse
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
}
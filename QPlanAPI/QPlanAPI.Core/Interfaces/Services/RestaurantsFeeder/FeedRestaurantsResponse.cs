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

}
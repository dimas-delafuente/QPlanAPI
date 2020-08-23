using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace QPlanAPI.DataAccess.Entities
{
    public class RestaurantEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("Location")]
        [BsonRequired]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        [BsonRequired]
        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonRequired]
        [BsonElement("City")]
        public string City { get; set; }

        [BsonRequired]
        [BsonElement("PostalCode")]
        public string PostalCode { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Categories")]
        public List<string> Categories { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("Rating")]
        public int Rating { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("CoverUrl")]
        public string CoverUrl { get; set; }

        [BsonRequired]
        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonIgnore]
        public BsonDouble Distance { get; set; }


    }
}

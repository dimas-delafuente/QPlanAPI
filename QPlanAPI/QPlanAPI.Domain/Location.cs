
namespace QPlanAPI.Domain
{
    public class Location
    {
        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

using System.Collections.Generic;

namespace QPlanAPI.Domain.Restaurants
{
    public class Restaurant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public RestaurantType Type { get; set; }

        public Location Location { get; set; }

        public double Distance { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public List<string> Categories { get; set; }

        public int Rating { get; set; }

        public string Url { get; set; }

        public string CoverUrl { get; set; }
    }

    #region Enums

    public enum RestaurantType
    {
        McDonalds,
        KFC,
        FostersHollywood,
        Ginos,
        TacoBell,
        PapaJohns,
        TGB,
        Subway,
        DominosPizza,
        Montaditos,
        LaSureña,
        Vips
    }

    #endregion Enums
}

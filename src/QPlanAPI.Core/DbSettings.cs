namespace QPlanAPI.Core
{
    public class DbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public DbCollections DatabaseCollections { get; set; }

        public class DbCollections {

            public string Restaurants { get; set; }
        }
    }


}

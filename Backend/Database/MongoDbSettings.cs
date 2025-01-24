namespace Backend.Database
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public CollectionsSettings Collections { get; set; }
    }

    public class CollectionsSettings
    {
        public string Products { get; set; }
        public string Orders { get; set; }
    }
}

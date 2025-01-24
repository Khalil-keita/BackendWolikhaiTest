using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Database
{

public class MongoDbContext
{
        private readonly IMongoDatabase _database;
        private readonly CollectionsSettings _collections;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var mongoSettings = settings.Value;
            var client = new MongoClient(mongoSettings.ConnectionString);
            _database = client.GetDatabase(mongoSettings.DatabaseName);
            _collections = mongoSettings.Collections;
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(_collections.Products);
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>(_collections.Orders);
    }
}

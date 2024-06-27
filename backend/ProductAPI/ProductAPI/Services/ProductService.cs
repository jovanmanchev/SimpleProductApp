using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Models.Config;

namespace ProductAPI.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<MongoDBSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _products = mongoDatabase.GetCollection<Product>("Products");
        }


        public List<Product> Get() => _products.Find(product => true).ToList();
        public Product Get(string id) => _products.Find<Product>(product => product.Id == id).FirstOrDefault();
        public Product Create(Product product) { _products.InsertOne(product); return product; }
        public void Update(string id, Product productIn) => _products.ReplaceOne(product => product.Id == id, productIn);
        public void Remove(Product productIn) => _products.DeleteOne(product => product.Id == productIn.Id);
        public void Remove(string id) => _products.DeleteOne(product => product.Id == id);
    }
}

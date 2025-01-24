using AutoMapper;
using Backend.Database;
using Backend.DTO;
using Backend.Models;
using Backend.Services.Interfaces;
using MongoDB.Driver;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMapper _mapper;

        public ProductService(MongoDbContext context, IMapper mapper)
        {
            _products = context.Products;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _products.Find(_ => true).ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products); 
        }

        public async Task<ProductDTO> GetProductByIdAsync(string id)
        {
            var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDTO>(product); 
        }
        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO); 
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            await _products.InsertOneAsync(product); 
            return _mapper.Map<ProductDTO>(product); 
        }

        public async Task UpdateProductAsync(string id, ProductDTO productDTO)
        {
            var existingProduct = await _products.Find(p => p.Id == id).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Product not found.");
            _mapper.Map(productDTO, existingProduct);
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _products.ReplaceOneAsync(p => p.Id == id, existingProduct); 
        }
        

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }
    }
}

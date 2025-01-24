using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(string id);
        Task<ProductDTO> CreateProductAsync(ProductDTO productDTO);
        Task UpdateProductAsync(string id, ProductDTO productDTO);
        Task DeleteProductAsync(string id);
    }
}

using Backend.DTO;
using Backend.Shared;

namespace Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDTO>>> GetAllProductsAsync();
        Task<Result<ProductDTO>> GetProductByIdAsync(string id);
        Task<Result<ProductDTO>> CreateProductAsync(ProductDTO productDTO);
        Task<Result<ProductDTO>> UpdateProductAsync(string id, ProductDTO productDTO);
        Task<Result<bool>> DeleteProductAsync(string id);
    }
}

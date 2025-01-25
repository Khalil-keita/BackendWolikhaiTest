using AutoMapper;
using Backend.DTO;
using Backend.Models;
using Backend.Services.Interfaces;
using Backend.Shared;
using MongoDB.Driver;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _products;
    private readonly IMapper _mapper;

    public ProductService(IMongoDatabase database, IMapper mapper)
    {
        _products = database.GetCollection<Product>("Products");
        _mapper = mapper;
    }

    public async Task<Result<ProductDTO>> CreateProductAsync(ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            await _products.InsertOneAsync(product);
            var resultDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.Success(resultDTO);
        }
        catch (Exception ex)
        {
            return Result<ProductDTO>.Failure($"Erreur lors de la création du produit : {ex.Message}");
        }
    }

    public async Task<Result<ProductDTO>> GetProductByIdAsync(string id)
    {
        try
        {
            var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return Result<ProductDTO>.Failure("Produit non trouvé");
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Result<ProductDTO>.Success(productDTO);
        }
        catch (Exception ex)
        {
            return Result<ProductDTO>.Failure($"Erreur lors de la récupération du produit : {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<ProductDTO>>> GetAllProductsAsync()
    {
        try
        {
            var products = await _products.Find(p => true).ToListAsync();
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Result<IEnumerable<ProductDTO>>.Success(productDTOs);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ProductDTO>>.Failure($"Erreur lors de la récupération des produits : {ex.Message}");
        }
    }

    public async Task<Result<ProductDTO>> UpdateProductAsync(string id, ProductDTO productDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(productDTO);
            var result = await _products.ReplaceOneAsync(p => p.Id == id, product);
            if (result.IsAcknowledged)
            {
                var updatedProductDTO = _mapper.Map<ProductDTO>(product);
                return Result<ProductDTO>.Success(updatedProductDTO);
            }
            return Result<ProductDTO>.Failure("Produit non trouvé ou échec de la mise à jour");
        }
        catch (Exception ex)
        {
            return Result<ProductDTO>.Failure($"Erreur lors de la mise à jour du produit : {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteProductAsync(string id)
    {
        try
        {
            var result = await _products.DeleteOneAsync(p => p.Id == id);
            if (result.IsAcknowledged)
            {
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Produit non trouvé ou échec de la suppression");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Erreur lors de la suppression du produit : {ex.Message}");
        }
    }
}

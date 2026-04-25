using CORM.Core.Repository;
using CORM.TestConsole.Entities;

namespace CORM.TestConsole.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryAsync<Product> _productRepository;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _productRepository = unitOfWork.RepositoryAsync<Product>();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(string name, decimal price, int stock)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            Stock = stock
        };
        
        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        
        return product;
    }

    public async Task<bool> UpdateStockAsync(int productId, int newStock)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return false;
        
        product.Stock = newStock;
        await _productRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}
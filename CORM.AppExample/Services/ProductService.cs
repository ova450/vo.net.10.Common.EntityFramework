using CORM.AppExample.Data;
using CORM.AppExample.Model;
using CORM.Core.Abstraction.Domain.Service;

namespace CORM.AppExample.Services
{
    public class ProductService(AppDbContext db) : ARepository<Product>(db) { }



    //public class ProductService : IUnitOfWork
    //{
    //    //private readonly IUnitOfWork _unitOfWork;
    //    //private readonly IRepositoryAsync<Product> _productRepository;

    //    //public ProductService(IUnitOfWork unitOfWork)
    //    //{
    //    //    _unitOfWork = unitOfWork;
    //    //    _productRepository = unitOfWork.RepositoryAsync<Product>();
    //    //}
        
    //    //public async Task<Product?> GetProductAsync(int id)
    //    //{
    //    //    return await _productRepository.GetByIdAsync(id);
    //    //}

    //    //public async Task<Product> CreateProductAsync(string name, decimal price, int stock)
    //    //{
    //    //    var product = new Product
    //    //    {
    //    //        Name = name,
    //    //        Price = price,
    //    //        Stock = stock
    //    //    };

    //    //    await _productRepository.AddAsync(product);
    //    //    await _unitOfWork.SaveChangesAsync();

    //    //    return product;
    //    //}

    //    //public async Task<bool> UpdateStockAsync(int productId, int newStock)
    //    //{
    //    //    var product = await _productRepository.GetByIdAsync(productId);
    //    //    if (product == null) return false;

    //    //    product.Stock = newStock;
    //    //    await _productRepository.UpdateAsync(product);
    //    //    await _unitOfWork.SaveChangesAsync();

    //    //    return true;
    //    //}
    //    private bool disposedValue;

    //    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int SaveChanges()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IRepositoryAsync<TEntity> IUnitOfWork.RepositoryAsync<TEntity>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposedValue)
    //        {
    //            if (disposing)
    //            {
    //                // TODO: освободить управляемое состояние (управляемые объекты)
    //            }

    //            // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
    //            // TODO: установить значение NULL для больших полей
    //            disposedValue = true;
    //        }
    //    }

    //    // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
    //    // ~ProductService()
    //    // {
    //    //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
    //    //     Dispose(disposing: false);
    //    // }

    //    public void Dispose()
    //    {
    //        // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
    //        Dispose(disposing: true);
    //        GC.SuppressFinalize(this);
    //    }
    //}
}
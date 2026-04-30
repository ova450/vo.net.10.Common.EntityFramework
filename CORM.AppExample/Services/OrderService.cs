using CORM.Core.Abstraction.Infrastructure;
using CORM.Core.Domain.Service;
using CORM.Core.Infrastructure;

namespace CORM.AppExample.Services
{
    public class OrderService(IContext context) : AUnitOfWork<IContext>(context)
    {
        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        {
            throw new NotImplementedException();
        }

        IRepositoryAsync<TEntity> IUnitOfWork.RepositoryAsync<TEntity>()
        {
            throw new NotImplementedException();
        }
    }
    //    public class OrderService(IUnitOfWork unitOfWork)
    //    {
    //        private readonly IUnitOfWork _unitOfWork = unitOfWork;
    //        private readonly IRepositoryAsync<Order> _orderRepository = unitOfWork.RepositoryAsync<Order>();
    //        private readonly IRepositoryAsync<Product> _productRepository = unitOfWork.RepositoryAsync<Product>();

    //        public async Task<Order> CreateOrderAsync(int userId, List<(int productId, int quantity)> items)
    //        {
    //            await _unitOfWork.BeginTransactionAsync();

    //            try
    //            {
    //                var order = new Order(userId, items)
    //                {
    //                    UserId = userId,
    //                    OrderDate = DateTime.UtcNow,
    //                    TotalAmount = 0
    //                };

    //                await _orderRepository.AddAsync(order);
    //                await _unitOfWork.SaveChangesAsync();

    //                decimal totalAmount = 0;

    //                foreach (var (productId, quantity) in items)
    //                {
    //                    var product = await _productRepository.GetByIdAsync(productId);
    //                    if (product == null)
    //                        throw new Exception($"Product {productId} not found");

    //                    if (product.Stock < quantity)
    //                        throw new Exception($"Insufficient stock for product {product.Name}");

    //                    var orderItem = new OrderItem
    //                    {
    //                        OrderId = order.Id,
    //                        ProductId = productId,
    //                        Quantity = quantity,
    //                        UnitPrice = product.Price
    //                    };

    //                    // This would be added through a repository for OrderItem
    //                    product.Stock -= quantity;
    //                    await _productRepository.UpdateAsync(product);

    //                    totalAmount += product.Price * quantity;
    //                }

    //                order.TotalAmount = totalAmount;
    //                await _orderRepository.UpdateAsync(order);
    //                await _unitOfWork.SaveChangesAsync();
    //                await _unitOfWork.CommitTransactionAsync();

    //                return order;
    //            }
    //            catch
    //            {
    //                await _unitOfWork.RollbackTransactionAsync();
    //                throw;
    //            }
    //        }
    //    }
    //}
}
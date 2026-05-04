using EntityNexus.Additionals.UnitOfWork;
using EntityNexus.DomainService;
using EntityNexus.Tests.AppExample.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Tests.AppExample.Services;

//public class OrderService(DbContext context) : AUnitOfWork<DbContext>(context)
//{
//    protected override int GetCurrentUserId()
//    {
//        throw new NotImplementedException();
//    }
//}

public class OrderService
{
    private readonly IUnitOfWork _uow;
    private readonly IRepositoryAsync<Order> _orders;
    private readonly IRepositoryAsync<Product> _products;

    public OrderService(IUnitOfWork uow)
    {
        _uow = uow;
        _orders = uow.RepositoryAsync<Order>();
        _products = uow.RepositoryAsync<Product>();
    }
}
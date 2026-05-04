using EntityNexus.Tests.AppExample.Data;
using EntityNexus.Tests.AppExample.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Tests.AppExample.Services;

public class OrderService(AppDbContext db)
{
    public async Task<Order> CreateOrderAsync(int userId, List<(int productId, int qty)> items)
    {
        var products = await db.Products
            .Where(p => items.Select(i => i.productId).Contains(p.Id))
            .ToListAsync<Product>();

        foreach (var (productId, qty) in items)
        {
            var product = products.First(p => p.Id == productId);

            if (product.Stock < qty) throw new InvalidOperationException("Not enough stock");

            product.Stock -= qty;
        }

        var order = new Order
        {
            ParentId = userId,
            TotalAmount = 0 // упростим пока
        };

        db.Orders.Add(order);

        await db.SaveChangesAsync();

        return order;
    }
}
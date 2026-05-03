using Microsoft.EntityFrameworkCore;

namespace ENM.Tests.AppExample.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : AppDbContexAbstract(options)
    {
        public required DbSet<User> Users;
        public required DbSet<Product> Products;
        public required DbSet<Order> Orders;
        public required DbSet<OrderItem> OrderItems;
    }
}
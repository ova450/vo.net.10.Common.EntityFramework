using CORM.AppExample.Model;
using Microsoft.EntityFrameworkCore;

namespace CORM.AppExample.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : AppDbContexAbstract(options)
    {
        public required DbSet<User> Users;
        public required DbSet<Product> Products;
        public required DbSet<Order> Orders;
        public required DbSet<OrderItem> OrderItems;
    }
}
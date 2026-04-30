using CORM.AppExample.Model;
using Microsoft.EntityFrameworkCore;

namespace CORM.AppExample.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : AppDbContexAbstract(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }
}
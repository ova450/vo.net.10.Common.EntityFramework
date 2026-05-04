using EntityNexus.Abstractions.Infrastructure;
using EntityNexus.Tests.AppExample.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Tests.AppExample.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : ADbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }
}
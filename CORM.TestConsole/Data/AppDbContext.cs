using CORM.EntityFrameworkCore.Storage;
using CORM.TestConsole.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORM.TestConsole.Data;

public class AppDbContext : AppDbContextBase
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}
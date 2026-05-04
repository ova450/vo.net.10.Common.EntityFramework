using EntityNexus.Abstractions.Domain.Model;
using EntityNexus.Abstractions.Infrastructure;
using EntityNexus.DomainModel;
using Microsoft.EntityFrameworkCore;

public class User : AEntityNamed;

public class Product(string name) : AEntityNamed
{
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class Order : AEntity, IHasParent<User>, IHasChild<OrderItem>
{
    public int ParentId { get; set; }
    public User? Parent { get; set; }

    public ICollection<OrderItem> Children { get; set; } = [];

    public decimal TotalAmount { get; set; }
}

public class OrderItem(string name) : AEntityNamed, IHasParent<Order>
{
    public int ParentId { get; set; }
    public Order? Parent { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class ShopDbContext(DbContextOptions<ShopDbContext> options)
    : ADbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}


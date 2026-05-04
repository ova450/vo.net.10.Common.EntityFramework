using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<ShopDbContext>(opt =>
    opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ENM_Sample;Trusted_Connection=True;"));

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();


// ✅ 1. Создаем пользователя
var user = new User
{
    Name = "John"
};

// ✅ 2. Создаем продукт
var product = new Product("Laptop")
{
    Name = "Laptop", // required!
    Price = 1000,
    Stock = 5
};

db.AddRange(user, product);
await db.SaveChangesAsync();


// ✅ 3. Создаем заказ
var order = new Order
{
    Parent = user,
    TotalAmount = product.Price,
    Children =
    [
        new OrderItem("Laptop")
        {
            Name = "Laptop", // required!
            Quantity = 1,
            Price = product.Price
        }
    ]
};

product.Stock -= 1;

db.Orders.Add(order);
await db.SaveChangesAsync();

Console.WriteLine("✅ Done");
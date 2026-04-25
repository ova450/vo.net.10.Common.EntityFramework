using CORM.Core.Repository;
using CORM.EntityFrameworkCore.Repository;
using CORM.TestConsole.Data;
using CORM.TestConsole.Entities;
using CORM.TestConsole.Services;
using CORM.TestConsole.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CORM.TestConsole;

class Program
{
    static async Task Main(string[] args)
    {
        
        ConsoleTee.StartLogging();  // Строка для включения логирования!

        try
        {
            Console.WriteLine("=== CORM Framework Test Console ===\n");

            // Setup dependency injection
            var services = new ServiceCollection();

            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register UnitOfWork with generic parameter
            services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

            // Register repositories if needed (optional, as they're created by UnitOfWork)
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            // Add services
            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();

            var serviceProvider = services.BuildServiceProvider();

            // Ensure database is created
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
                Console.WriteLine("✅ Database created successfully\n");
            }

            // Run demo
            await RunDemo(serviceProvider);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }
        finally
        {
            ConsoleTee.StopLogging();
        }
    }

    static async Task RunDemo(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
        var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Demo 1: Create users
        Console.WriteLine("📝 Creating users...");
        var user1 = await userService.CreateUserAsync("John Doe", "john@example.com");
        var user2 = await userService.CreateUserAsync("Jane Smith", "jane@example.com");
        Console.WriteLine($"✅ Created users: {user1.Name} (ID: {user1.Id}), {user2.Name} (ID: {user2.Id})\n");

        // Demo 2: Create products
        Console.WriteLine("📦 Creating products...");
        var product1 = await productService.CreateProductAsync("Laptop", 999.99m, 10);
        var product2 = await productService.CreateProductAsync("Mouse", 29.99m, 50);
        var product3 = await productService.CreateProductAsync("Keyboard", 79.99m, 30);
        Console.WriteLine($"✅ Created products: {product1.Name} (${product1.Price}), {product2.Name} (${product2.Price}), {product3.Name} (${product3.Price})\n");

        // Demo 3: Get all users
        Console.WriteLine("📋 Getting all users...");
        var allUsers = await userService.GetAllUsersAsync();
        foreach (var user in allUsers)
        {
            Console.WriteLine($"   - {user.Name} ({user.Email})");
        }
        Console.WriteLine();

        // Demo 4: Get specific user
        Console.WriteLine($"🔍 Getting user by ID {user1.Id}...");
        var foundUser = await userService.GetUserAsync(user1.Id);
        Console.WriteLine($"   Found: {foundUser?.Name}\n");

        // Demo 5: Update user
        Console.WriteLine("✏️ Updating user...");
        if (foundUser != null)
        {
            foundUser.Name = "John Updated";
            await userService.UpdateUserAsync(foundUser);
            var updatedUser = await userService.GetUserAsync(user1.Id);
            Console.WriteLine($"   Updated name: {updatedUser?.Name}\n");
        }

        // Demo 6: Update product stock
        Console.WriteLine("📊 Updating product stock...");
        await productService.UpdateStockAsync(product1.Id, 5);
        var updatedProduct = await productService.GetProductAsync(product1.Id);
        Console.WriteLine($"   {updatedProduct?.Name} new stock: {updatedProduct?.Stock}\n");

        // Demo 7: Create order with transaction
        Console.WriteLine("🛒 Creating order with transaction...");
        try
        {
            var items = new List<(int productId, int quantity)>
            {
                (product1.Id, 2),
                (product2.Id, 3)
            };

            var order = await orderService.CreateOrderAsync(user1.Id, items);
            Console.WriteLine($"✅ Order created! ID: {order.Id}, Total: ${order.TotalAmount}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Order failed: {ex.Message}\n");
        }

        // Demo 8: Using UnitOfWork directly
        Console.WriteLine("💾 Using UnitOfWork directly...");
        var productRepo = unitOfWork.RepositoryAsync<Product>();
        var cheapProducts = await productRepo.FindAsync(p => p.Price < 100);
        Console.WriteLine($"   Cheap products (< $100): {cheapProducts.Count}");
        foreach (var product in cheapProducts)
        {
            Console.WriteLine($"      - {product.Name}: ${product.Price}");
        }
        Console.WriteLine();

        // Demo 9: Count entities
        var userCount = await unitOfWork.RepositoryAsync<User>().CountAsync();
        var productCount = await unitOfWork.RepositoryAsync<Product>().CountAsync();
        Console.WriteLine($"📊 Statistics:");
        Console.WriteLine($"   Total users: {userCount}");
        Console.WriteLine($"   Total products: {productCount}");
        var orderCount = await unitOfWork.RepositoryAsync<Order>().CountAsync();
        Console.WriteLine($"   Total orders: {orderCount}");

        // Demo 10: Demonstrate transaction rollback
        Console.WriteLine("\n🔄 Testing transaction rollback...");
        try
        {
            await unitOfWork.BeginTransactionAsync();

            // Create a product that will be rolled back
            var tempProduct = new Product
            {
                Name = "Temp Product",
                Price = 999.99m,
                Stock = 100
            };
            var tempRepo = unitOfWork.RepositoryAsync<Product>();
            await tempRepo.AddAsync(tempProduct);
            await unitOfWork.SaveChangesAsync();

            Console.WriteLine($"   Created temporary product: {tempProduct.Name} (ID: {tempProduct.Id})");

            // Throw exception to test rollback
            throw new InvalidOperationException("Simulated error - rolling back transaction");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            Console.WriteLine($"   Transaction rolled back: {ex.Message}");

            // Verify product count after rollback (should be same as before)
            var finalProductCount = await unitOfWork.RepositoryAsync<Product>().CountAsync();
            Console.WriteLine($"   Product count after rollback: {finalProductCount} (should be {productCount})");
        }
    }
}
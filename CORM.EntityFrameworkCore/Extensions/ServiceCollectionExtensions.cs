using CORM.Core.Repository;
using CORM.EntityFrameworkCore.Repository;
using CORM.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CORM.EntityFrameworkCore.Extensions;

/// <summary>
/// Dependency injection extensions for CORM
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add CORM Unit of Work and repositories to the service collection
    /// </summary>
    /// <summary>
/// Add CORM Unit of Work and repositories to the service collection
/// </summary>
public static IServiceCollection AddCormUnitOfWork<TContext>(
    this IServiceCollection services,
    IConfiguration configuration,
    string connectionStringName = "DefaultConnection")
    where TContext : DbContext
{
    var connectionString = configuration.GetConnectionString(connectionStringName)
        ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found");

    services.AddDbContext<TContext>(options =>
        options.UseSqlServer(connectionString));
    
    services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
    
    return services;
}
    
    /// <summary>
    /// Add CORM with custom DbContext configuration
    /// </summary>
    public static IServiceCollection AddCormUnitOfWork<TContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
        where TContext : AppDbContext
    {
        services.AddDbContext<TContext>(optionsAction);
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        
        return services;
    }
}
//using CORM.Domain.Core.Services;
//using CORM.EntityFrameworkCore.Repository;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace CORM.EntityFrameworkCore.Extensions;

//public static class ServiceCollectionExtensions
//{
//    public static IServiceCollection AddCormUnitOfWork<TContext>(
//        this IServiceCollection services,
//        IConfiguration configuration,
//        string connectionStringName = "DefaultConnection")
//        where TContext : DbContext
//    {
//        var connectionString = configuration.GetConnectionString(connectionStringName)
//            ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found");

//        services.AddDbContext<TContext>(options =>
//            options.UseSqlServer(connectionString));

//        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
//        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

//        return services;
//    }
//    public static IServiceCollection AddCormUnitOfWork<TContext>(
//    this IServiceCollection services,
//    Action<DbContextOptionsBuilder> optionsAction)
//    where TContext : DbContext
//    {
//        services.AddDbContext<TContext>(optionsAction);
//        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
//        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

//        return services;
//    }
//}
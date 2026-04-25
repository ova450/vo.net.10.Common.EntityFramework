using System.Reflection;
using CORM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORM.EntityFrameworkCore.Storage;

/// <summary>
/// Base DbContext with automatic entity configuration
/// </summary>
public class AppDbContext : DbContext
{
    private readonly Assembly? _entitiesAssembly;
    private readonly string[] _primaryKeys;

    public AppDbContext(
        DbContextOptions options,
        Assembly? entitiesAssembly = null,
        string[]? primaryKeys = null) : base(options)
    {
        _entitiesAssembly = entitiesAssembly ?? Assembly.GetCallingAssembly();
        _primaryKeys = primaryKeys ?? ["Id"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Auto-configure entities from assembly
        if (_entitiesAssembly != null)
        {
            var entityTypes = _entitiesAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface)
                .Where(t => typeof(IEntityBase).IsAssignableFrom(t));
            
            foreach (var entityType in entityTypes)
            {
                var builder = modelBuilder.Entity(entityType);
                
                // Set primary key if not set
                if (builder.Metadata.FindPrimaryKey() == null)
                    builder.HasKey(_primaryKeys);
            }
        }
        
        // Apply configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
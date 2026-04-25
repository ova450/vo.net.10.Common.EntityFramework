using System.Reflection;
using CORM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CORM.EntityFrameworkCore.Storage;

/// <summary>
/// Base DbContext with automatic entity configuration
/// </summary>
public abstract class AppDbContextBase : DbContext
{
    private readonly string[] _primaryKeys;

    protected AppDbContextBase(
        DbContextOptions options,
        string[]? primaryKeys = null) : base(options)
    {
        _primaryKeys = primaryKeys ?? ["Id"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        
        // Auto-configure primary keys for entities that don't have configuration
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.FindPrimaryKey() == null && 
                entityType.ClrType.GetInterface(nameof(IEntityBase)) != null)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasKey(_primaryKeys);
            }
        }
    }
}
using EntityNexus.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityNexus.Abstractions.Infrastructure;

public abstract class ADbContext(DbContextOptions options, string[]? primaryKeys = null) : DbContext(options)
{
    private readonly string[] _primaryKeys = primaryKeys ?? ["Id"];

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Применяем все конфигурации из сборки
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // 1. Primary Key
            if (entityType.FindPrimaryKey() == null &&
                clrType.GetInterface(nameof(IEntity)) != null)
            {
                modelBuilder.Entity(clrType).HasKey(_primaryKeys);
            }

            // 2. Concurrency
            if (typeof(IConcurrency).IsAssignableFrom(clrType))
            {
                modelBuilder.Entity(clrType)
                    .Property<byte[]>("RowVersion")
                    .IsRowVersion();
            }

            // 3. Soft Delete
            if (typeof(ISoftDeletable).IsAssignableFrom(clrType) ||
                clrType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISoftDeletable<,>)))
            {
                modelBuilder.Entity(clrType).HasQueryFilter(e =>
                    EF.Property<bool>(e, nameof(ISoftDeletable.IsDeleted)) == false);
            }

            // 4. Атрибуты
            var auditableAttr = clrType.GetCustomAttribute<AuditableAttribute>();
            if (auditableAttr != null)
            {
                // Можно добавить дополнительные конвенции для Auditable
            }

            var trackHistoryAttr = clrType.GetCustomAttribute<TrackHistoryAttribute>();
            if (trackHistoryAttr != null)
            {
                // Настройка истории изменений
            }
        }
    }
}
using CORM.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace CORM.Core.Abstraction.Infrastructure;

public abstract class ADbContext(DbContextOptions options, string[]? primaryKeys = null) : DbContext(options)
{
    private readonly string[] _primaryKeys = primaryKeys ?? ["Id"];

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Auto-configure primary keys for entities that don't have configuration
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clr = entityType.ClrType;

            if (entityType.FindPrimaryKey() == null &&
                clr.GetInterface(nameof(IEntityBase)) != null) modelBuilder.Entity(clr).HasKey(_primaryKeys);
            //typeof(IEntityBase).IsAssignableFrom(clr))

            // 3. Concurrency (RowVersion) для IConcurrency
            if (typeof(IConcurrency).IsAssignableFrom(clr)) modelBuilder.Entity(clr).Property<byte[]>("RowVersion").IsRowVersion();

            // 4. Soft delete: глобальный фильтр для IEntityDeleted
            if (typeof(IEntityDeleted).IsAssignableFrom(clr))
            {
                var parameter = Expression.Parameter(clr, "e");
                var prop = Expression.Call(
                    typeof(EF),
                    nameof(EF.Property),
                    [typeof(bool)],
                    parameter,
                    Expression.Constant("IsDeleted"));

                var compare = Expression.Equal(prop, Expression.Constant(false));
                var lambda = Expression.Lambda(compare, parameter);

                modelBuilder.Entity(clr).HasQueryFilter(lambda);
            }
        }
    }
}

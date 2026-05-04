using EntityNexus.Additionals;
using EntityNexus.Additionals.History;
using EntityNexus.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityNexus.Abstractions.Infrastructure;

public abstract class ADbContext(DbContextOptions options, string[]? primaryKeys = null) : DbContext(options)
{
    private readonly string[] _primaryKeys = primaryKeys ?? ["Id"];

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;

            if (entry.State == EntityState.Added)
            {
                if (entity is ICreated created) created.CreatedAt = now;// временно, см. ниже
            }

            if (entry.State == EntityState.Modified)
            {
                if (entity is IModified modified) modified.ModifiedAt = now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Применяем все конфигурации из сборки
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var isAuditable = clrType.GetCustomAttribute<AuditableAttribute>() != null;

            if (isAuditable)
            {
                modelBuilder.Entity(clrType).Property<DateTimeOffset>("CreatedAt");
                modelBuilder.Entity(clrType).Property<DateTimeOffset?>("ModifiedAt");
            }

            // 2. Concurrency
            if (typeof(IConcurrency).IsAssignableFrom(clrType))
                modelBuilder.Entity(clrType).Property<byte[]>("RowVersion").IsRowVersion();

            //// 3. Soft Delete
            //if (typeof(ISoftDeletable).IsAssignableFrom(clrType) ||
            //    clrType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISoftDeletable)))
            //{
            //    modelBuilder.Entity(clrType).HasQueryFilter(e => EF.Property<bool>(e, nameof(ISoftDeletable.IsDeleted)) == false);
            //}

            var hasParent = clrType.GetInterfaces().FirstOrDefault(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHasParent<,>));

            if (hasParent != null)
            {
                var parentType = hasParent.GetGenericArguments()[0];

                modelBuilder.Entity(clrType)
                    .HasOne(parentType)
                    .WithMany()
                    .HasForeignKey("ParentId")
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}

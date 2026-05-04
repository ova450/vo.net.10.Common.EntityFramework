using EntityNexus.Additionals;
using EntityNexus.Additionals.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityNexus.Abstractions.Infrastructure;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// Настраивает сущность как Auditable (Created + Modified)
    /// </summary>
    public static EntityTypeBuilder<TEntity> Auditable<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ICreated
    {
        builder.Property(e => e.CreatedAt).IsRequired();

        //builder.Property(e => e.CreatedBy).IsRequired();

        return builder;
    }

    /// <summary>
    /// Настраивает Soft Delete
    /// </summary>
    public static EntityTypeBuilder<TEntity> SoftDeletable<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        builder.Property(e => e.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);

        return builder;
    }

    /// <summary>
    /// Настраивает Optimistic Concurrency
    /// </summary>
    public static EntityTypeBuilder<TEntity> Concurrency<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IConcurrency
    {
        builder.Property(e => e.RowVersion).IsRowVersion();

        return builder;
    }
}
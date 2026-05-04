using EntityNexus.Additionals.History;
using EntityNexus.DomainModel;
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
        builder.Property(e => e.CreatedAt)
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .IsRequired();

        return builder;
    }

    /// <summary>
    /// Настраивает Soft Delete
    /// </summary>
    public static EntityTypeBuilder<TEntity> SoftDeletable<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        builder.Property(e => e.IsDeleted)
               .HasDefaultValue(false)
               .IsRequired();

        builder.HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);

        return builder;
    }

    /// <summary>
    /// Настраивает Optimistic Concurrency
    /// </summary>
    public static EntityTypeBuilder<TEntity> Concurrency<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IConcurrency
    {
        builder.Property(e => e.RowVersion)
               .IsRowVersion();

        return builder;
    }

    ///// <summary>
    ///// Настраивает историю изменений (отдельная таблица)
    ///// </summary>
    //public static EntityTypeBuilder<TEntity> HasHistory<TEntity, THistory>(this EntityTypeBuilder<TEntity> builder)
    //    where TEntity : class
    //    where THistory : class, IModified<TEntity, /*TUser*/>
    //{
    //    builder.HasMany<THistory>()
    //           .WithOne()
    //           .HasForeignKey("ParentId")   // или явно указать
    //           .OnDelete(DeleteBehavior.Cascade);

    //    return builder;
    //}

    /// <summary>
    /// Универсальный метод для применения всех конвенций
    /// </summary>
    public static EntityTypeBuilder<TEntity> ApplyEntityNexusConventions<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IEntity
    {
        // Primary Key уже настраивается в базовом классе
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        // Если сущность реализует несколько интерфейсов — применяем их
        if (typeof(ICreated).IsAssignableFrom(typeof(TEntity)))
            builder.Auditable();

        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            builder.SoftDeletable();

        if (typeof(IConcurrency).IsAssignableFrom(typeof(TEntity)))
            builder.Concurrency();

        return builder;
    }
}
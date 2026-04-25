using CORM.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORM.EntityFrameworkCore.Storage;

/// <summary>
/// Base entity configuration with automatic primary key setup
/// </summary>
public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntityBase
{
    protected virtual string[] PrimaryKeys => ["Id"];
    protected virtual bool UseDefaultPrimaryKeyConfiguration => true;
    
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        if (UseDefaultPrimaryKeyConfiguration)
            builder.HasKey(PrimaryKeys);
        
        ConfigureEntity(builder);
    }
    
    /// <summary>
    /// Override to configure entity relationships and properties
    /// </summary>
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
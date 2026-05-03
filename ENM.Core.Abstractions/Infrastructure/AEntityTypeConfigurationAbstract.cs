using EntityNexus.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityNexus.Abstractions.Infrastructure
{
    /// <summary>
    /// Base entity configuration with automatic primary key setup
    /// </summary>
    public abstract class AEntityTypeConfigurationAbstract<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
    {
        protected virtual string[] PrimaryKeys => ["Id"];
        protected virtual bool UseDefaultPrimaryKeyConfiguration => true;
    
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (UseDefaultPrimaryKeyConfiguration) builder.HasKey(PrimaryKeys);
        
            ConfigureEntity(builder);
        }
    
        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
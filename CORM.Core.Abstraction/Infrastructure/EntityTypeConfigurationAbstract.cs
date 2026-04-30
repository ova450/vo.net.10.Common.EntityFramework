using CORM.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORM.Core.Abstraction.Infrastructure
{
    /// <summary>
    /// Base entity configuration with automatic primary key setup
    /// </summary>
    public abstract class EntityTypeConfigurationAbstract<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity
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
}
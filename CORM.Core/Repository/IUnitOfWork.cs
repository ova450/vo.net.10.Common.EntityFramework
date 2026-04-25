using CORM.Core.Entities;

namespace CORM.Core.Repository;

/// <summary>
/// Unit of Work pattern for transaction management
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>Get repository for entity type</summary>
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityBase;
    
    /// <summary>Get async repository for entity type</summary>
    IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IEntityBase;
    
    /// <summary>Save changes synchronously</summary>
    int SaveChanges();
    
    /// <summary>Save changes asynchronously</summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Begin transaction</summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Commit transaction</summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Rollback transaction</summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
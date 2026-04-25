using System.Linq.Expressions;
using CORM.Core.Entities;

namespace CORM.Core.Repository;

/// <summary>
/// Asynchronous repository pattern
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
{
    /// <summary>Get queryable asynchronously</summary>
    Task<IQueryable<TEntity>> QueryAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Get all entities asynchronously</summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Get entity by id asynchronously</summary>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>Add entity asynchronously</summary>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>Update entity asynchronously</summary>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>Remove entity asynchronously</summary>
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    /// <summary>Remove entity by id asynchronously</summary>
    Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>Count entities asynchronously</summary>
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Check if entity exists asynchronously</summary>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>Find entities by predicate asynchronously</summary>
    Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);
    
    /// <summary>Get first entity matching predicate</summary>
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}
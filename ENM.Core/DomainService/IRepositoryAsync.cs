using EntityNexus.DomainModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace EntityNexus.DomainService
{
    public interface IRepositoryAsync<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        ValueTask<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        //Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<EntityEntry> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<EntityEntry> RemoveAsync(TKey id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<long> CountAsyncLong(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
    }

    public interface IRepositoryAsync<TEntity> : IRepositoryAsync<TEntity, int> 
        where TEntity : class, IEntity;
}

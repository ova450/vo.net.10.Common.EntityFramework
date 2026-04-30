using CORM.Core.Domain.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CORM.Core.Domain.Service
{
    public interface IRepositoryBaseAsync<TEntity, TKey> 
        where TEntity : class, IEntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IQueryable<TEntity>> QueryAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        ValueTask<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
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

    public interface IRepositoryBaseAsync<TEntity> : IRepositoryBaseAsync<TEntity, int> 
        where TEntity : class, IEntityBase;

    public interface IRepositoryAsync<TEntity, TKey> : IRepositoryBaseAsync<TEntity, TKey> 
        where TEntity : class, IEntity<TKey> 
        where TKey : IEquatable<TKey> ;

    public interface IRepositoryAsync<TEntity> : IRepositoryBaseAsync<TEntity,int> 
        where TEntity : class, IEntity;
}

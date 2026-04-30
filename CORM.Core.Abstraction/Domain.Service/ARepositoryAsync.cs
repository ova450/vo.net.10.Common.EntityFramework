using CORM.Core.Domain.Model;
using CORM.Core.Domain.Service;
using CORM.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CORM.Core.Abstraction.Domain.Service;

public abstract class ARepositoryAsync<TEntity, TKey>(DbContext context)
    : IRepositoryAsync<TEntity, TKey>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    protected readonly DbContext db = context;
    protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await dbSet.AsNoTracking().ToListAsync(cancellationToken);
    public virtual async ValueTask<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => await dbSet.FindAsync([id], cancellationToken);
    public virtual async ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => (await dbSet.AddAsync(entity, cancellationToken)).Entity;

    //public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    //{
    //    var entry = dbSet.Update(entity);
    //    return Task.FromResult(entry.Entity);
    //}

    public virtual async Task<EntityEntry> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = dbSet.Remove(entity);
        return await Task.FromResult(entry);
    }
    public virtual async Task<EntityEntry> RemoveAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity == null
            ? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} not found")
            : dbSet.Remove(entity);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        => await dbSet.CountAsync(cancellationToken);
    public virtual async Task<long> CountAsyncLong(CancellationToken cancellationToken = default)
        => await dbSet.LongCountAsync(cancellationToken);

    public virtual async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default)
        => await dbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
}

public abstract class ARepositoryAsync<TEntity>(DbContext context)    : ARepositoryAsync<TEntity, int>(context)
    where TEntity : class, IEntity<int>;


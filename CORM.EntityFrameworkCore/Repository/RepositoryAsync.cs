using System.Linq.Expressions;
using CORM.Core.Entities;
using CORM.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace CORM.EntityFrameworkCore.Repository;

/// <summary>
/// Entity Framework implementation of asynchronous repository
/// </summary>
public class RepositoryAsync<TEntity> : Repository<TEntity>, IRepositoryAsync<TEntity>
    where TEntity : class, IEntityBase
{
    public RepositoryAsync(DbContext context) : base(context)
    {
    }

    public virtual async Task<IQueryable<TEntity>> QueryAsync(CancellationToken cancellationToken = default)
        => await Task.Run(() => Query(), cancellationToken);
    
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    
    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);
    
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await _dbSet.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }
    
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _dbSet.Update(entity);
        return await Task.FromResult(entry.Entity);
    }
    
    public virtual async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return await Task.FromResult(true);
    }
    
    public virtual async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null) return false;
        return await RemoveAsync(entity, cancellationToken);
    }
    
    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        => await _dbSet.CountAsync(cancellationToken);
    
    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    
    public virtual async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
    
    public virtual async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
}
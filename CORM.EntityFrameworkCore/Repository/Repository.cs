using System.Linq.Expressions;
using CORM.Core.Entities;
using CORM.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace CORM.EntityFrameworkCore.Repository;

/// <summary>
/// Entity Framework implementation of synchronous repository
/// </summary>
public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class, IEntityBase
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Query() => _dbSet.AsNoTracking();
    
    public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var include in includes)
            query = query.Include(include);
        return query.AsNoTracking();
    }

    public virtual IEnumerable<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();
    
    public virtual TEntity? GetById(int id) => _dbSet.Find(id);
    
    public virtual TEntity Add(TEntity entity)
    {
        var entry = _dbSet.Add(entity);
        return entry.Entity;
    }
    
    public virtual TEntity Update(TEntity entity)
    {
        var entry = _dbSet.Update(entity);
        return entry.Entity;
    }
    
    public virtual bool Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        return true;
    }
    
    public virtual bool Remove(int id)
    {
        var entity = GetById(id);
        if (entity == null) return false;
        return Remove(entity);
    }
    
    public virtual int Count() => _dbSet.Count();
    
    public virtual bool Exists(int id) => _dbSet.Any(e => e.Id == id);
    
    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) 
        => _dbSet.Where(predicate).AsNoTracking().ToList();
    
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        => _dbSet.FirstOrDefault(predicate);
}
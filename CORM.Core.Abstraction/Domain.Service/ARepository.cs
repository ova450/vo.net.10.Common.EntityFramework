using CORM.Core.Domain.Model;
using CORM.Core.Domain.Service;
using CORM.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CORM.Core.Abstraction.Domain.Service;

public abstract class ARepository<TEntity, TKey>(DbContext context) : IRepository<TEntity, TKey>
    where TKey : struct, IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    protected readonly DbContext db = (context ?? throw new ArgumentNullException(nameof(context)));
    protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public virtual IQueryable<TEntity> Query() => dbSet.AsNoTracking();

    public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = dbSet;
        foreach (var include in includes)
            query = query.Include(include);
        return query.AsNoTracking();
    }

    public TEntity Add(TEntity entity) => db.Add(entity).Entity;
    public int Count() => dbSet.Count();
    public long CountLong() => dbSet.LongCount();
    public bool Exists(TKey id) => dbSet.Any(e => e.Id.Equals(id));
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => dbSet.Where(predicate).ToList();
    public IEnumerable<TEntity> GetAll() => dbSet.ToList();

    public TEntity? GetById(TKey id) => dbSet.FirstOrDefault(e => e.Id.Equals(id));

    public EntityEntry Remove(TEntity entity) => db.Remove(entity);

    public EntityEntry Remove(TKey id)=>db.Remove(dbSet.FirstOrDefault(e => e.Id.Equals(id)) 
        ?? throw new ArgumentException($"Entity with id {id} not found.", nameof(id)));

    public TEntity Update(TEntity entity) => db.Update(entity).Entity;
}

public abstract class ARepository<TEntity>(DbContext context) : ARepository<TEntity, int>(context)
    where TEntity : class, IEntity<int>;

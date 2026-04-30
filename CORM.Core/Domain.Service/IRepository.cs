using CORM.Core.Domain.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CORM.Core.Domain.Service
{
    public interface IRepositoryBase<TEntity, TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(TKey id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        EntityEntry Remove(TEntity entity);
        EntityEntry Remove(TKey id);
        int Count();
        long CountLong();
        bool Exists(TKey id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IRepositoryBase<TEntity> where TEntity : class, IEntityBase { }
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey> { }
    public interface IRepository<TEntity> where TEntity : class, IEntity { }

}

using System.Linq.Expressions;
using CORM.Core.Entities;

namespace CORM.Core.Repository;

/// <summary>
/// Synchronous repository pattern
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntityBase
{
    /// <summary>Get queryable for advanced queries</summary>
    IQueryable<TEntity> Query();
    
    /// <summary>Get queryable with includes</summary>
    IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);
    
    /// <summary>Get all entities</summary>
    IEnumerable<TEntity> GetAll();
    
    /// <summary>Get entity by id</summary>
    TEntity? GetById(int id);
    
    /// <summary>Add entity</summary>
    TEntity Add(TEntity entity);
    
    /// <summary>Update entity</summary>
    TEntity Update(TEntity entity);
    
    /// <summary>Remove entity</summary>
    bool Remove(TEntity entity);
    
    /// <summary>Remove entity by id</summary>
    bool Remove(int id);
    
    /// <summary>Count entities</summary>
    int Count();
    
    /// <summary>Check if entity exists</summary>
    bool Exists(int id);
    
    /// <summary>Find entities by predicate</summary>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
}
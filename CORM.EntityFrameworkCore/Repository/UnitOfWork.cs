using CORM.Core.Entities;
using CORM.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CORM.EntityFrameworkCore.Repository;

/// <summary>
/// Entity Framework implementation of Unit of Work
/// </summary>
/// <typeparam name="TContext">Type of DbContext</typeparam>
public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private readonly Dictionary<Type, object> _repositoriesAsync;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
        _repositoriesAsync = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityBase
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
            _repositories[type] = new Repository<TEntity>(_context);
        
        return (IRepository<TEntity>)_repositories[type];
    }

    public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IEntityBase
    {
        var type = typeof(TEntity);
        if (!_repositoriesAsync.ContainsKey(type))
            _repositoriesAsync[type] = new RepositoryAsync<TEntity>(_context);
        
        return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
    }

    public int SaveChanges() => _context.SaveChanges();
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
            throw new InvalidOperationException("Transaction already started");
            
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction to commit");

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction to rollback");

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
        _disposed = true;
    }
}
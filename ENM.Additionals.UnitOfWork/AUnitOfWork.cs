using EntityNexus.Additionals.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityNexus.Additionals.UnitOfWork;

public abstract class AUnitOfWork<TContext>(TContext context)
    : IUnitOfWork, ITransactionAsync
    where TContext : DbContext
{
    protected readonly TContext db = context ?? throw new ArgumentNullException(nameof(context));

    private IDbContextTransaction? _transaction;
    private bool _disposed;

    protected abstract int GetCurrentUserId();

    #region SaveChanges

    public int SaveChanges()
    {
        ApplyAudit();
        return db.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();
        return await db.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAudit()
    {
        var now = DateTimeOffset.UtcNow;
        var userId = GetCurrentUserId();

        foreach (var entry in db.ChangeTracker.Entries())
        {
            // Created
            if (entry.Entity is ICreated created && entry.State == EntityState.Added)
            {
                created.CreatedAt = now;
                created.CreatedBy = userId;
            }

            // Deleted (Soft Delete)
            if (entry.Entity is IDeleted deleted && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                deleted.DeletedAt = now;
                deleted.DeletedBy = userId;
            }

            // Modified (для основных сущностей)
            if (entry.Entity is IModified modified &&
                (entry.State == EntityState.Modified || entry.State == EntityState.Added))
            {
                modified.ModifiedAt = now;
                modified.ModifiedBy = userId;
            }
        }
    }

    #endregion

    #region Transaction

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
            throw new InvalidOperationException("Transaction already started");

        _transaction = await db.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new InvalidOperationException("No transaction to commit");

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new InvalidOperationException("No transaction to rollback");

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    #endregion

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
            db.Dispose();
        }
        _disposed = true;
    }
}
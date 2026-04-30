using CORM.Core.Domain.Model;
using CORM.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace CORM.Core.Abstraction.Infrastructure;

public abstract class AUnitOfWork<TContext> (TContext context): IUnitOfWork, ITransactionAsync
    where TContext : DbContext
{
    protected readonly TContext db = context ?? throw new ArgumentNullException(nameof(context));

    private IDbContextTransaction? _transaction;
    private bool _disposed;

    /// <summary>
    /// Текущий пользователь для аудита.
    /// Реализация в инфраструктуре (например, через ICurrentUserService).
    /// </summary>
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
            if (entry.Entity is IEntityCreated created && entry.State == EntityState.Added)
            {
                created.CreatedAt = now;
                created.CreatedBy = userId;
            }

            if (entry.Entity is IEntityModified modified &&
                (entry.State == EntityState.Modified || entry.State == EntityState.Added))
            {
                modified.ModifiedAt = now;
                modified.ModifiedBy = userId;
            }

            if (entry.Entity is IEntityDeleted deleted && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                deleted.IsDeleted = true;
                deleted.DeletedAt = now;
                deleted.DeletedBy = userId;
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

    #endregion

    #region Dispose

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

    #endregion
}

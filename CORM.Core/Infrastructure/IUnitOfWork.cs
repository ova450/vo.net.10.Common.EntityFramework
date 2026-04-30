namespace CORM.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable 
    { 
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}

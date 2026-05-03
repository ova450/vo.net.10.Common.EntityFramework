namespace EntityNexus.Additionals.UnitOfWork
{
    public interface IUnitOfWork : IDisposable 
    { 
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}

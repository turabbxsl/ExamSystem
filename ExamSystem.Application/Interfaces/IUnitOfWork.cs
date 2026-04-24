public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    IGenericRepository<T> GetRepository<T>() where T : class;

    Task<int> SaveChangesAsync();
}
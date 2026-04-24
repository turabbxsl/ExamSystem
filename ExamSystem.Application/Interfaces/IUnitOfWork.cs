public interface IUnitOfWork : IDisposable
{
    /* IGenericRepository<Course> Courses { get; }
     IGenericRepository<Student> Students { get; }
     IGenericRepository<Exam> Exams { get; }
     Task<int> SaveChangesAsync();*/


    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    IGenericRepository<T> GetRepository<T>() where T : class;

    Task<int> SaveChangesAsync();
}
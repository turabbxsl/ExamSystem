using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    private readonly Dictionary<string, object> _repositories = new();

    public async Task BeginTransactionAsync() => _transaction = await context.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync()
    {
        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    public async Task RollbackTransactionAsync()
    {
        await _transaction!.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repository = new GenericRepository<T>(context);
            _repositories.Add(type, repository);
        }
        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
    public void Dispose() => context.Dispose();

    /*private IGenericRepository<Course>? _courses;
    private IGenericRepository<Student>? _students;
    private IGenericRepository<Exam>? _exams;


    public IGenericRepository<Course> Courses => _courses ??= new GenericRepository<Course>(context);
    public IGenericRepository<Student> Students => _students ??= new GenericRepository<Student>(context);
    public IGenericRepository<Exam> Exams => _exams ??= new GenericRepository<Exam>(context);


    public void Dispose() => context.Dispose();

    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    */
}
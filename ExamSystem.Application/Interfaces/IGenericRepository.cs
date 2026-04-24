using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(string[] includes);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(int id, string[]? includes = null);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
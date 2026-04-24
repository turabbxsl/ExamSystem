using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T>
where T : class
{

    private readonly DbSet<T> _set = context.Set<T>();



    // Get all entities of type T from the database asynchronously.
    public async Task<IEnumerable<T>> GetAllAsync() => await _set.AsNoTracking().ToListAsync();


    // Get all entities of type T from the database asynchronously, including related entities specified in the includes parameter.
    public async Task<IEnumerable<T>> GetAllAsync(string[] includes)
    {
        IQueryable<T> query = _set.AsNoTracking();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }


    // Get an entity of type T by its ID from the database asynchronously.
    public async Task<T?> GetByIdAsync(int id) =>
       await _set.FindAsync(id);


    // Get an entity of type T by its ID from the database asynchronously, including related entities specified in the includes parameter.
    public async Task<T?> GetByIdAsync(int id, string[]? includes = null)
    {
        IQueryable<T> query = _set;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
    }

    // Find entities of type T that match a given predicate from the database asynchronously.
    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate) =>
         await _set.AsNoTracking().Where(predicate).ToListAsync();


    // Find a single entity of type T that matches a given predicate from the database asynchronously.
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _set.FirstOrDefaultAsync(predicate);


    // Add a new entity of type T to the database asynchronously.
    public async Task AddAsync(T entity) => await _set.AddAsync(entity);


    // Delete an existing entity of type T from the database.
    public void Delete(T entity) =>
        _set.Remove(entity);


    // Update an existing entity of type T in the database.
    public void Update(T entity) =>
         _set.Update(entity);

}
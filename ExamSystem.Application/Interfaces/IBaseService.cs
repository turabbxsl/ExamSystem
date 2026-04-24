public interface IBaseService<TEntity, TDto> 
{
    Task<Result<IEnumerable<TDto>>> GetAllAsync();
    Task<Result<TDto>> GetByIdAsync(int id);
    Task<Result<bool>> DeleteAsync(int id);
}
public abstract class BaseService<TEntity, TDto>(IUnitOfWork unitOfWork) : IBaseService<TEntity, TDto>
    where TEntity : class
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    protected IGenericRepository<TEntity> Repository => _unitOfWork.GetRepository<TEntity>();

    protected virtual string[] GetIncludes() => [];

    public virtual async Task<Result<IEnumerable<TDto>>> GetAllAsync()
    {
        var includes = GetIncludes();

        var entities = await Repository.GetAllAsync(includes);

        return Result<IEnumerable<TDto>>.Success(MapToDtoList(entities));
    }

    public virtual async Task<Result<TDto>> GetByIdAsync(int id)
    {
        var includes = GetIncludes();
        var entity = await Repository.GetByIdAsync(id, includes);
        if (entity is null)
            return Result<TDto>.NotFound($"{typeof(TEntity).Name} not found.");

        return Result<TDto>.Success(MapToDto(entity));
    }

    public virtual async Task<Result<bool>> DeleteAsync(int id)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity is null)
                return Result<bool>.NotFound($"{typeof(TEntity).Name} not found.");

            Repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            return Result<bool>.Success(true);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<bool>.Failure("An error occurred while deleting the entity.");
        }
    }

    protected async Task<Result<TResult>> ExecuteTransactionAsync<TResult>(Func<Task<Result<TResult>>> action)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var result = await action();
            if (result.IsSuccess)
                await _unitOfWork.CommitTransactionAsync();
            else
                await _unitOfWork.RollbackTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<TResult>.Failure("An unexpected error occurred.");
        }
    }

    protected async Task<Result<T>> RollbackFailure<T>(string message)
    {
        await _unitOfWork.RollbackTransactionAsync();
        return Result<T>.Failure(message);
    }

    protected async Task<Result<T>> RollbackNotFound<T>(string message)
    {
        await _unitOfWork.RollbackTransactionAsync();
        return Result<T>.NotFound(message);
    }


    protected abstract TDto MapToDto(TEntity entity);
    protected abstract IEnumerable<TDto> MapToDtoList(IEnumerable<TEntity> entities);
}
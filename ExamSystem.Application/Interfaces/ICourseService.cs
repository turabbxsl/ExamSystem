public interface ICourseService
{
    Task<Result<IEnumerable<CourseDto>>> GetAllAsync();
    Task<Result<CourseDto>> GetByIdAsync(int id);
    Task<Result<CourseDto>> CreateAsync(CreateCourseDto dto);
    Task<Result<CourseDto>> UpdateAsync(int id, UpdateCourseDto dto);
    Task<Result<bool>> DeleteAsync(int id);
}
public interface IStudentService
{
    Task<Result<IEnumerable<StudentDto>>> GetAllAsync();
    Task<Result<StudentDto>> GetByIdAsync(int id);
    Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto);
    Task<Result<StudentDto>> UpdateAsync(int id, UpdateStudentDto dto);
    Task<Result<bool>> DeleteAsync(int id);
}
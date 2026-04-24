public interface IExamService
{
    Task<Result<IEnumerable<ExamDto>>> GetAllAsync();
    Task<Result<ExamDto>> GetByIdAsync(int id);
    Task<Result<ExamDto>> CreateAsync(CreateExamDto dto);
    Task<Result<ExamDto>> UpdateAsync(int id, UpdateExamDto dto);
    Task<Result<bool>> DeleteAsync(int id);
}
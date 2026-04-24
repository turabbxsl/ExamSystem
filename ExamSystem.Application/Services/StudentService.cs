

namespace ExamSystem.Application.Services;

public class StudentService(IUnitOfWork unitOfWork)
    : BaseService<Student, StudentDto>(unitOfWork), IStudentService
{
    public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var exists = await Repository.FindAsync(s => s.StudentNumber == dto.StudentNumber);
            if (exists is not null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<StudentDto>.Failure($"A student with number '{dto.StudentNumber}' already exists.");
            }

            var student = new Student
            {
                StudentNumber = dto.StudentNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                GradeLevel = dto.GradeLevel
            };

            await Repository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            return Result<StudentDto>.Success(MapToDto(student), 201);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<StudentDto>.Failure("Error occurred while creating the student.");
        }
    }

    public async Task<Result<StudentDto>> UpdateAsync(int id, UpdateStudentDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var student = await Repository.GetByIdAsync(id);
            if (student is null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<StudentDto>.NotFound($"Student not found. Id: {id}");
            }

            // Nömrə dəyişimi üçün yoxlama
            if (student.StudentNumber != dto.StudentNumber)
            {
                var exists = await Repository.FindAsync(s => s.StudentNumber == dto.StudentNumber);
                if (exists is not null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<StudentDto>.Failure($"Already exists a student with number '{dto.StudentNumber}'.");
                }
            }

            student.StudentNumber = dto.StudentNumber;
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.GradeLevel = dto.GradeLevel;
            student.UpdatedAt = DateTime.UtcNow;

            Repository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            return Result<StudentDto>.Success(MapToDto(student));
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<StudentDto>.Failure("Error occurred while updating the student.");
        }
    }

    protected override StudentDto MapToDto(Student s) => new(
        s.Id, s.StudentNumber, s.FirstName, s.LastName, s.GradeLevel);

    protected override IEnumerable<StudentDto> MapToDtoList(IEnumerable<Student> entities)
        => entities.Select(MapToDto);
}
namespace ExamSystem.Application.Services;

public class StudentService(IUnitOfWork unitOfWork)
    : BaseService<Student, StudentDto>(unitOfWork), IStudentService
{
    public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            var exists = await Repository.FindAsync(s => s.StudentNumber == dto.StudentNumber);
            if (exists is not null)
                return await RollbackFailure<StudentDto>($"A student with number '{dto.StudentNumber}' already exists.");

            var student = new Student
            {
                StudentNumber = dto.StudentNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                GradeLevel = dto.GradeLevel
            };

            await Repository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            return Result<StudentDto>.Success(MapToDto(student), 201);
        });
    }

    public async Task<Result<StudentDto>> UpdateAsync(int id, UpdateStudentDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            var student = await Repository.GetByIdAsync(id);
            if (student is null)
                return await RollbackNotFound<StudentDto>($"Student not found. Id: {id}");

            if (student.StudentNumber != dto.StudentNumber)
            {
                var exists = await Repository.FindAsync(s => s.StudentNumber == dto.StudentNumber);
                if (exists is not null)
                    return await RollbackFailure<StudentDto>($"A student with number '{dto.StudentNumber}' already exists.");
            }

            student.StudentNumber = dto.StudentNumber;
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.GradeLevel = dto.GradeLevel;

            Repository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return Result<StudentDto>.Success(MapToDto(student));
        });
    }

    protected override StudentDto MapToDto(Student s) => new(
        s.Id, s.StudentNumber, s.FirstName, s.LastName, s.GradeLevel);

    protected override IEnumerable<StudentDto> MapToDtoList(IEnumerable<Student> entities)
        => entities.Select(MapToDto);
}
public class CourseService(IUnitOfWork unitOfWork)
    : BaseService<Course, CourseDto>(unitOfWork), ICourseService
{
    public async Task<Result<CourseDto>> CreateAsync(CreateCourseDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var exists = await Repository.FindAsync(c => c.CourseCode == dto.CourseCode);
            if (exists is not null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<CourseDto>.Failure($"'{dto.CourseCode}' code already exists.");
            }

            var course = new Course
            {
                CourseCode = dto.CourseCode,
                CourseName = dto.CourseName,
                GradeLevel = dto.GradeLevel,
                TeacherFirstName = dto.TeacherFirstName,
                TeacherLastName = dto.TeacherLastName
            };

            await Repository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            return Result<CourseDto>.Success(MapToDto(course), 201);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<CourseDto>.Failure("Error occurred while creating the course.");
        }
    }

    public async Task<Result<CourseDto>> UpdateAsync(int id, UpdateCourseDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var course = await Repository.GetByIdAsync(id);
            if (course is null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<CourseDto>.NotFound($"Course not found.");
            }

            course.CourseName = dto.CourseName;
            course.GradeLevel = dto.GradeLevel;
            course.TeacherFirstName = dto.TeacherFirstName;
            course.TeacherLastName = dto.TeacherLastName;
            course.UpdatedAt = DateTime.UtcNow;

            Repository.Update(course);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();
            return Result<CourseDto>.Success(MapToDto(course));
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Result<CourseDto>.Failure("Error occurred while updating the course.");
        }
    }

    protected override CourseDto MapToDto(Course c) => new(
        c.Id, c.CourseCode, c.CourseName, c.GradeLevel, c.TeacherFirstName, c.TeacherLastName);

    protected override IEnumerable<CourseDto> MapToDtoList(IEnumerable<Course> entities)
        => entities.Select(MapToDto);
}
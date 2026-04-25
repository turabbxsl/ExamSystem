public class CourseService(IUnitOfWork unitOfWork)
    : BaseService<Course, CourseDto>(unitOfWork), ICourseService
{
    public async Task<Result<CourseDto>> CreateAsync(CreateCourseDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            var exists = await Repository.FindAsync(c => c.CourseCode == dto.CourseCode);
            if (exists is not null)
                return await RollbackFailure<CourseDto>($"'{dto.CourseCode}' code already exists.");

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

            return Result<CourseDto>.Success(MapToDto(course), 201);
        });
    }

    public async Task<Result<CourseDto>> UpdateAsync(int id, UpdateCourseDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            var course = await Repository.GetByIdAsync(id);
            if (course is null)
                return await RollbackNotFound<CourseDto>("Course not found.");

            course.CourseName = dto.CourseName;
            course.GradeLevel = dto.GradeLevel;
            course.TeacherFirstName = dto.TeacherFirstName;
            course.TeacherLastName = dto.TeacherLastName;

            Repository.Update(course);
            await _unitOfWork.SaveChangesAsync();

            return Result<CourseDto>.Success(MapToDto(course));
        });
    }

    protected override CourseDto MapToDto(Course c) => new(
        c.Id, c.CourseCode, c.CourseName, c.GradeLevel, c.TeacherFirstName, c.TeacherLastName);

    protected override IEnumerable<CourseDto> MapToDtoList(IEnumerable<Course> entities)
        => entities.Select(MapToDto);
}
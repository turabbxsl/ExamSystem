public class ExamService(IUnitOfWork unitOfWork)
    : BaseService<Exam, ExamDto>(unitOfWork), IExamService
{
    protected override string[] GetIncludes() => new[] { nameof(Exam.Course), nameof(Exam.Student) };

    public async Task<Result<ExamDto>> CreateAsync(CreateExamDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            if (await _unitOfWork.GetRepository<Course>().GetByIdAsync(dto.CourseId) is null)
                return await RollbackNotFound<ExamDto>($"Course not found. Id: {dto.CourseId}");

            if (await _unitOfWork.GetRepository<Student>().GetByIdAsync(dto.StudentId) is null)
                return await RollbackNotFound<ExamDto>($"Student not found. Id: {dto.StudentId}");

            var exam = new Exam
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                ExamDate = dto.ExamDate,
                Score = dto.Score
            };

            await Repository.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();

            var createdExam = await Repository.GetByIdAsync(exam.Id, GetIncludes());
            return Result<ExamDto>.Success(MapToDto(createdExam!), 201);
        });
    }

    public async Task<Result<ExamDto>> UpdateAsync(int id, UpdateExamDto dto)
    {
        return await ExecuteTransactionAsync(async () =>
        {
            var exam = await Repository.GetByIdAsync(id, GetIncludes());

            if (exam is null)
                return await RollbackNotFound<ExamDto>($"Exam not found. Id: {id}");

            exam.Score = dto.Score;
            exam.ExamDate = dto.ExamDate;

            Repository.Update(exam);
            await _unitOfWork.SaveChangesAsync();

            return Result<ExamDto>.Success(MapToDto(exam));
        });
    }

    protected override ExamDto MapToDto(Exam e) => new(
        Id: e.Id,
        CourseCode: e.Course?.CourseCode ?? "N/A",
        CourseName: e.Course?.CourseName ?? "N/A",
        StudentNumber: e.Student?.StudentNumber ?? 0,
        StudentFullName: $"{e.Student?.FirstName} {e.Student?.LastName}",
        ExamDate: e.ExamDate,
        Score: e.Score
    );

    protected override IEnumerable<ExamDto> MapToDtoList(IEnumerable<Exam> entities)
        => entities.Select(MapToDto);
}
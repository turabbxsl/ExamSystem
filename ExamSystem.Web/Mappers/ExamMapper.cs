public static class ExamMapper
{
    public static ExamViewModel ToViewModel(this ExamDto dto) => new()
    {
        Id = dto.Id,
        CourseCode = dto.CourseCode,
        CourseName = dto.CourseName,
        StudentNumber = dto.StudentNumber,
        StudentFullName = dto.StudentFullName,
        ExamDate = dto.ExamDate,
        Score = dto.Score
    };

    public static CreateExamDto ToDto(this CreateExamViewModel vm) => new(
        vm.CourseId,
        vm.StudentId,
        vm.ExamDate,
        vm.Score
    );

    public static UpdateExamDto ToUpdateDto(this UpdateExamViewModel vm) => new(
    vm.ExamDate,
    vm.Score
);
}
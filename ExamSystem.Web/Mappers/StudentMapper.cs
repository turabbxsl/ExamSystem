public static class StudentMapper
{
    public static StudentViewModel ToViewModel(this StudentDto dto) => new()
    {
        Id = dto.Id,
        StudentNumber = dto.StudentNumber,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        GradeLevel = dto.GradeLevel
    };

    public static CreateStudentDto ToDto(this CreateStudentViewModel vm) => new(
        vm.StudentNumber,
        vm.FirstName,
        vm.LastName,
        vm.GradeLevel
    );

    public static UpdateStudentDto ToDto(this UpdateStudentViewModel vm) => new(
        vm.StudentNumber,
        vm.FirstName,
        vm.LastName,
        vm.GradeLevel
    );
}
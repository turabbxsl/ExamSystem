public static class CourseMapper
{
    public static CourseViewModel ToViewModel(this CourseDto dto) => new()
    {
        Id = dto.Id,
        CourseCode = dto.CourseCode,
        CourseName = dto.CourseName,
        GradeLevel = dto.GradeLevel,
        TeacherFullName = $"{dto.TeacherFirstName} {dto.TeacherLastName}"
    };

    public static CreateCourseDto ToDto(this CreateCourseViewModel vm) => new(
        vm.CourseCode,
        vm.CourseName,
        vm.GradeLevel,
        vm.TeacherFirstName,
        vm.TeacherLastName
    );

    public static UpdateCourseDto ToDto(this UpdateCourseViewModel vm) => new(
        vm.CourseName,
        vm.GradeLevel,
        vm.TeacherFirstName,
        vm.TeacherLastName
    );

}
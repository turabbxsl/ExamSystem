public record CourseDto(
    int Id,
    string CourseCode,
    string CourseName,
    byte GradeLevel,
    string TeacherFirstName,
    string TeacherLastName
);

public record CreateCourseDto(
    string CourseCode,
    string CourseName,
    byte GradeLevel,
    string TeacherFirstName,
    string TeacherLastName
);

public record UpdateCourseDto(
    string CourseName,
    byte GradeLevel,
    string TeacherFirstName,
    string TeacherLastName
);
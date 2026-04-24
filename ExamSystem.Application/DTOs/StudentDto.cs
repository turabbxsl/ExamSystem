public record StudentDto(
    int Id,
    int StudentNumber,
    string FirstName,
    string LastName,
    byte GradeLevel
);

public record CreateStudentDto(
    int StudentNumber,
    string FirstName,
    string LastName,
    byte GradeLevel
);

public record UpdateStudentDto(
    int StudentNumber,
    string FirstName,
    string LastName,
    byte GradeLevel
);
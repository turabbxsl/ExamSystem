public record ExamDto(
    int Id,
    string CourseCode,
    string CourseName,
    int StudentNumber,
    string StudentFullName,
    DateOnly ExamDate,
    byte Score
);

public record CreateExamDto(
    int CourseId,
    int StudentId,
    DateOnly ExamDate,
    byte Score
);

public record UpdateExamDto(
    DateOnly ExamDate,
    byte Score);
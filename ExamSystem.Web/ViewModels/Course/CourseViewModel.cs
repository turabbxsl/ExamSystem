

public sealed class CourseViewModel
{
    public int Id { get; init; }
    public string CourseCode { get; init; } = string.Empty;
    public string CourseName { get; init; } = string.Empty;
    public byte GradeLevel { get; init; }
    public string TeacherFullName { get; init; } = string.Empty;
}

public sealed class CreateCourseViewModel
{
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public byte GradeLevel { get; set; }
    public string TeacherFirstName { get; set; } = string.Empty;
    public string TeacherLastName { get; set; } = string.Empty;
}

public sealed class UpdateCourseViewModel
{
    public int Id { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public byte GradeLevel { get; set; }
    public string TeacherFirstName { get; set; } = string.Empty;
    public string TeacherLastName { get; set; } = string.Empty;
}

public sealed class CourseListViewModel
{
    public IEnumerable<CourseViewModel> Courses { get; init; } = [];
    public string PageTitle { get; init; } = "Dərslər";
}
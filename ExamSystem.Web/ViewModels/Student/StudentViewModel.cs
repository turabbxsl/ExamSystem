

public sealed class StudentViewModel
{
    public int Id { get; init; }
    public int StudentNumber { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public byte GradeLevel { get; init; }
}


public sealed class CreateStudentViewModel
{
    public int StudentNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte GradeLevel { get; set; }
}


public sealed class UpdateStudentViewModel
{
    public int Id { get; set; }
    public int StudentNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte GradeLevel { get; set; }
}

public sealed class StudentListViewModel
{
    public IEnumerable<StudentViewModel> Students { get; init; } = [];
    public string PageTitle { get; init; } = "Şagirdlər";
}
using Microsoft.AspNetCore.Mvc.Rendering;

public sealed class ExamViewModel
{
    public int Id { get; init; }
    public string CourseCode { get; init; } = string.Empty;
    public string CourseName { get; init; } = string.Empty;
    public int StudentNumber { get; init; }
    public string StudentFullName { get; init; } = string.Empty;
    public DateOnly ExamDate { get; init; }
    public byte Score { get; init; }
}


public sealed class CreateExamViewModel
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public DateOnly ExamDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public byte Score { get; set; }

    // For dropdown lists in the view
    public SelectList? CourseSelectList { get; set; }
    public SelectList? StudentSelectList { get; set; }
}


public sealed class UpdateExamViewModel
{
    public int Id { get; set; }
    public DateOnly ExamDate { get; set; }
    public byte Score { get; set; }
}

public sealed class ExamListViewModel
{
    public IEnumerable<ExamViewModel> Exams { get; init; } = [];
    public string PageTitle { get; init; } = "İmtahanlar";
}
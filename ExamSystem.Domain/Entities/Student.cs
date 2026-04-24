public class Student : BaseEntity
{
    public int StudentNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte GradeLevel { get; set; }

    public ICollection<Exam> Exams { get; set; } = [];
}
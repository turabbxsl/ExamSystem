public class Exam : BaseEntity
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public DateOnly ExamDate { get; set; }
    public byte Score { get; set; }


    public Course Course { get; set; } = null!;
    public Student Student { get; set; } = null!;
}
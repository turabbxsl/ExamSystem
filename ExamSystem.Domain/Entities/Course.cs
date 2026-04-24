public class Course:BaseEntity
{
    public string CourseCode { get; set; } = string.Empty;  
    public string CourseName { get; set; } = string.Empty;      
    public byte GradeLevel  { get; set; }                    
    public string TeacherFirstName { get; set; } = string.Empty; 
    public string TeacherLastName  { get; set; } = string.Empty; 


    public ICollection<Exam> Exams { get; set; } = [];

}
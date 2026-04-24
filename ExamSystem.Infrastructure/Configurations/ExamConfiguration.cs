using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Score)
               .HasColumnType("tinyint")
               .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_Exam_Score", "[Score] BETWEEN 0 AND 9"));

        builder.HasOne(x => x.Course)
               .WithMany(c => c.Exams)
               .HasForeignKey(x => x.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Student)
               .WithMany(s => s.Exams)
               .HasForeignKey(x => x.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
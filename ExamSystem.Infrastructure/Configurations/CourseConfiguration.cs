using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.CourseCode);
        builder.Property(x => x.CourseCode).HasColumnType("char(3)").IsRequired();

        builder.Property(x => x.CourseName).HasMaxLength(30).IsRequired();
        builder.Property(x => x.GradeLevel).HasColumnType("tinyint");

        builder.Property(x => x.TeacherFirstName).HasMaxLength(20).IsRequired();
        builder.Property(x => x.TeacherLastName).HasMaxLength(20).IsRequired();
    }
}
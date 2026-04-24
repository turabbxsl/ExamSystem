using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamSystem.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.StudentNumber);

        builder.Property(x => x.StudentNumber)
               .IsRequired();

        builder.Property(x => x.FirstName)
               .HasMaxLength(30)
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasMaxLength(30)
               .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_Student_StudentNumber", 
            "[StudentNumber] BETWEEN 1 AND 99999"));
    }
}
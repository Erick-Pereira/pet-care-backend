using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class ExamMapConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("exam");

            builder.Property(e => e.ExamType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Result)
                .HasMaxLength(500);

            builder.Property(e => e.Notes)
                .HasMaxLength(1000);

            builder.Property(e => e.ExamDate)
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(e => e.MedicalEvent)
                .WithMany()
                .HasForeignKey(e => e.MedicalEventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
  internal class DiagnosisMapConfig : IEntityTypeConfiguration<Diagnosis>
  {
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
      builder.ToTable("diagnosis");

      builder.Property(d => d.Condition)
          .IsRequired()
          .HasMaxLength(100);

      builder.Property(d => d.Severity)
          .IsRequired()
          .HasMaxLength(50);

      builder.Property(d => d.recommendedTreatment)
          .IsRequired()
          .HasMaxLength(500)
          .HasColumnName("recommended_treatment");

      builder.Property(d => d.Notes)
          .HasMaxLength(1000);

      builder.HasOne(d => d.MedicalEvent)
          .WithMany()
          .HasForeignKey(d => d.MedicalEventId)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
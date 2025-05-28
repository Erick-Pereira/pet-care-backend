using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class MedicationMapConfig : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("medication");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Dosage)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Frequency)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Notes)
                .HasMaxLength(1000);

            builder.HasOne(m => m.MedicalEvent)
                .WithMany()
                .HasForeignKey(m => m.MedicalEventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
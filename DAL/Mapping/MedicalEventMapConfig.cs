using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class MedicalEventMapConfig : IEntityTypeConfiguration<MedicalEvent>
    {
        public void Configure(EntityTypeBuilder<MedicalEvent> builder)
        {
            builder.ToTable("medical_event");

            builder.Property(m => m.ProfessionalName)
                .HasMaxLength(100);

            builder.Property(m => m.Description)
                .HasMaxLength(500);

            builder.Property(m => m.Notes)
                .HasMaxLength(1000);

            builder.HasOne(m => m.Pet)
                .WithMany()
                .HasForeignKey(m => m.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
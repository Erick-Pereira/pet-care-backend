using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class VaccineMapConfig : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable("vaccine");

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Batch)
                .HasMaxLength(50);

            builder.HasOne(v => v.MedicalEvent)
                .WithMany()
                .HasForeignKey(v => v.MedicalEventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
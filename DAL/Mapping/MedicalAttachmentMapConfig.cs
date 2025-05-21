using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class MedicalAttachmentMapConfig : IEntityTypeConfiguration<MedicalAttachment>
    {
        public void Configure(EntityTypeBuilder<MedicalAttachment> builder)
        {
            builder.ToTable("medical_attachment");

            builder.Property(m => m.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.FileData)
                .IsRequired();

            builder.HasOne(m => m.MedicalEvent)
                .WithMany(e => e.Attachments)
                .HasForeignKey(m => m.MedicalEventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
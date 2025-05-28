using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class DocumentMapConfig : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("document");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.DocumentNumber)
                .HasMaxLength(50);

            builder.Property(d => d.IssuingAgency)
                .HasMaxLength(100);

            builder.HasOne(d => d.Pet)
                .WithMany()
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
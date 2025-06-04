using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class PetPhotoMapConfig : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("pet_photo");

            builder.Property(p => p.PhotoData)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Date)
                .IsRequired();

            builder.HasOne(p => p.Pet)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
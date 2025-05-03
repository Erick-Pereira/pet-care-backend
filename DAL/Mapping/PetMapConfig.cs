using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class PetMapConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable(PetConstants.TableName);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(PetConstants.NameMaxLength);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(PetConstants.GenderMaxLength);

            builder.Property(p => p.Color)
                .HasMaxLength(PetConstants.ColorMaxLength);

            builder.Property(p => p.Acquisition)
                .HasMaxLength(PetConstants.AcquisitionMaxLength);

            builder.Property(p => p.IsCastrated)
                .IsRequired();

            builder.Property(p => p.IsChipped)
                .IsRequired();

            builder.Property(p => p.ChipNumber)
                .HasMaxLength(PetConstants.ChipNumberMaxLength);

            builder.Property(p => p.ApproximateBirthDate);

            builder.HasOne(p => p.Specie)
                .WithMany()
                .HasForeignKey(p => p.SpecieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Breed)
                .WithMany()
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Owner)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
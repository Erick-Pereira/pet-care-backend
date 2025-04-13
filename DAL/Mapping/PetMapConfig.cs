using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class PetMapConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pet");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.Color)
                .HasMaxLength(50);

            builder.Property(p => p.Acquisition)
                .HasMaxLength(50);

            builder.Property(p => p.IsCastrated)
                .IsRequired();

            builder.Property(p => p.ApproximateBirthDate);

            builder.HasOne(p => p.Specie)
                .WithMany()
                .HasForeignKey(p => p.SpecieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Breed)
                .WithMany()
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
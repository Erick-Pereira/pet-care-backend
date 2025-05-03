using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class BreedMapConfig : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable(BreedConstants.TableName);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(BreedConstants.NameMaxLength);

            builder.Property(b => b.Description)
                .HasMaxLength(BreedConstants.DescriptionMaxLength);

            builder.HasOne(b => b.Specie)
                .WithMany()
                .HasForeignKey(b => b.SpeciesId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
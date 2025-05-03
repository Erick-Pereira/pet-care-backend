using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class SpecieMapConfig : IEntityTypeConfiguration<Specie>
    {
        public void Configure(EntityTypeBuilder<Specie> builder)
        {
            builder.ToTable(SpecieConstants.TableName);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(SpecieConstants.NameMaxLength);

            builder.Property(s => s.Description)
                .HasMaxLength(SpecieConstants.DescriptionMaxLength);

            builder.HasIndex(s => s.Name)
                .IsUnique()
                .HasDatabaseName(SpecieConstants.UniqueIndexName);
        }
    }
}
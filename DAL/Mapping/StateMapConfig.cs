using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class StateMapConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable(StateConstants.TableName);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(StateConstants.NameMaxLength);

            builder.Property(s => s.Abbreviation)
                .IsRequired()
                .HasMaxLength(StateConstants.AbbreviationMaxLength)
                .IsUnicode(false);

            builder.HasIndex(s => s.Abbreviation)
                .IsUnique()
                .HasDatabaseName(StateConstants.AbbreviationUniqueIndexName);

            builder.HasIndex(s => s.Name)
                .IsUnique()
                .HasDatabaseName(StateConstants.NameUniqueIndexName);
        }
    }
}
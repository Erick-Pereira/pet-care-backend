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

            builder.Property(s => s.Abreviation)
                .IsRequired()
                .HasMaxLength(StateConstants.AbreviationMaxLength)
                .IsUnicode(false);

            builder.HasIndex(s => s.Abreviation)
                .IsUnique()
                .HasDatabaseName(StateConstants.AbreviationUniqueIndexName);

            builder.HasIndex(s => s.Name)
                .IsUnique()
                .HasDatabaseName(StateConstants.NameUniqueIndexName);
        }
    }
}
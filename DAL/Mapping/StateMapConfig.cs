using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class StateMapConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("state");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Abreviation)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);

            builder.HasIndex(s => s.Abreviation)
                .IsUnique();
        }
    }
}
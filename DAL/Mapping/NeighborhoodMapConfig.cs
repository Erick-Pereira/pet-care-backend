using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    public class NeighborhoodMapConfig : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> builder)
        {
            builder.ToTable("neighborhood");
            builder.Property(b => b.Name).IsUnicode(false).IsRequired();
        }
    }
}
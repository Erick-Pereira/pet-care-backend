using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class NeighborhoodMapConfig : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> builder)
        {
            builder.ToTable(NeighborhoodConstants.TableName);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(NeighborhoodConstants.NameMaxLength)
                .IsUnicode(false);

            builder.HasOne(n => n.City)
                .WithMany()
                .HasForeignKey(n => n.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class CityMapConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(CityConstants.TableName);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(CityConstants.NameMaxLength);

            builder.HasOne(c => c.State)
                .WithMany()
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
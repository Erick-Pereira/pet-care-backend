using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class AddressMapConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(8);

            builder.HasOne(a => a.Neighborhood)
                .WithMany()
                .HasForeignKey(a => a.NeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class AddressMapConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(AddressConstants.TableName);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(AddressConstants.StreetMaxLength);

            builder.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(AddressConstants.NumberMaxLength);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(AddressConstants.ZipCodeLength);

            builder.HasOne(a => a.Neighborhood)
                .WithMany()
                .HasForeignKey(a => a.NeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
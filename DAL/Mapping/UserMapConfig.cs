using Commons.Constants;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    internal class UserMapConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(UserConstants.TableName);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(UserConstants.FullNameMaxLength);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(UserConstants.EmailMaxLength);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(UserConstants.PhoneNumberMaxLength);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName(UserConstants.EmailUniqueIndexName);
        }
    }
}
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
            builder.ToTable("user");

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(UserConstants.FullNameMaxLength);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(UserConstants.EmailMaxLength);

            builder.Property(u => u.PhoneNumber)
                .IsRequired();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataMapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(12);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(8);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Phone).IsRequired().HasMaxLength(16);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(12);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(12);
            builder.HasMany(u => u.Expenses).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
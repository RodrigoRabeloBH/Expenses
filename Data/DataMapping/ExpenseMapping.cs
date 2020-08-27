using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.DataMapping
{
    public class ExpenseMapping : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(24);
            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(200);
            builder.Property(e => e.DueDate).IsRequired();
        }
    }
}
using Data.DataMapping;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Expense> Expense { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
    }
}
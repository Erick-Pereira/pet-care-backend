using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataBaseDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DataBaseDbContext(DbContextOptions<DataBaseDbContext> options)
: base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql("getdate()");
            base.OnModelCreating(modelBuilder);
        }
    }
}
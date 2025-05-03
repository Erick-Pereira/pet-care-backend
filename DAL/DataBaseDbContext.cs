using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL
{
    public class DataBaseDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Breed> Breed { get; set; }
        public DbSet<Neighborhood> Neighborhood { get; set; }
        public DbSet<Specie> Specie { get; set; }

        public DataBaseDbContext(DbContextOptions<DataBaseDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
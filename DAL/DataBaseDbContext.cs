using DAL.Mapping;
using Entities;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.ApplyConfiguration(new UserMapConfig());
            modelBuilder.ApplyConfiguration(new AddressMapConfig());
            modelBuilder.ApplyConfiguration(new NeighborhoodMapConfig());
            modelBuilder.ApplyConfiguration(new CityMapConfig());
            modelBuilder.ApplyConfiguration(new StateMapConfig());
            modelBuilder.ApplyConfiguration(new BreedMapConfig());
            modelBuilder.ApplyConfiguration(new SpecieMapConfig());
            modelBuilder.ApplyConfiguration(new PetMapConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
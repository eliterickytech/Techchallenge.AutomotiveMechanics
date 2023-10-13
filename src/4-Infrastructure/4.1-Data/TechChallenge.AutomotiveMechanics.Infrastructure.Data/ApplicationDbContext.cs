using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.SeedData;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Car> Car { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("AutomotiveMechanics"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Seed();
        }
    }
}

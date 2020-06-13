using Microsoft.EntityFrameworkCore;
using TekusClientsAPI.Models;

namespace TekusClientsAPI.Infrastructure
{
    public class ClientsContext: DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options): base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ServiceCountry> ServiceCountries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceCountry>()
                .HasKey(sc => new { sc.ServiceId, sc.CountryId });

            modelBuilder.Entity<ServiceCountry>()
                .HasOne(sc => sc.Service)
                .WithMany(s => s.ServiceCountries)
                .HasForeignKey(sc => sc.ServiceId);

            modelBuilder.Entity<ServiceCountry>()
                .HasOne(sc => sc.Country)
                .WithMany(c => c.ServiceCountries)
                .HasForeignKey(sc => sc.CountryId);


            base.OnModelCreating(modelBuilder);
        }
    }
}

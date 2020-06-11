using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekusClientsAPI.Models;

namespace TekusClientsAPI.Infrastructure
{
    public class ClientsContext: DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options): base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<CountryService> CountryServices { get; set; }
    }
}

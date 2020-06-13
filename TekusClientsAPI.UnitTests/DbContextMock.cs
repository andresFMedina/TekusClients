using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TekusClientsAPI.Infrastructure;

namespace TekusClientsAPI.UnitTests
{
    static class DbContextMock
    {
        public static ClientsContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ClientsContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new ClientsContext(options);

            dbContext.Seed();

            return dbContext;
        }
    }
}

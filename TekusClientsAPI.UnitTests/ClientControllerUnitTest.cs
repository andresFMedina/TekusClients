using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TekusClientsAPI.Controllers;
using TekusClientsAPI.Models;
using TekusClientsAPI.Utils;
using Xunit;

namespace TekusClientsAPI.UnitTests
{
    public class ClientControllerUnitTest
    {
        [Fact]
        public async Task TestGetClientsAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetClientsAsync));
            var controller = new ClientController(dbContext);

            var response = await controller.GetClientsAsync("") as ObjectResult;
            var value = response.Value as IPagedResponse<Client>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestGetClientsByIdAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetClientsByIdAsync));
            var controller = new ClientController(dbContext);

            var response = await controller.GetClientByIdAsync(1) as ObjectResult;
            var value = response.Value as ISingleResponse<Client>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestPostClientAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestPostClientAsync));
            var controller = new ClientController(dbContext);

            var client = new Client
            {
                Id = 0,
                Name = "Mi cliente nuevo de prueba",
                Nit = "8002949-8",
                Email = "nuevocliente@gmail.com",
            };

            var response = await controller.PostClientAsync(client) as ObjectResult;
            var value = response.Value as ISingleResponse<Client>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestPutClientAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestPutClientAsync));
            var controller = new ClientController(dbContext);

            var client = new Client
            {
                Id = 1,
                Name = "Mi cliente nuevo de prueba modificado",
                Nit = "8002949-8",
                Email = "nuevocliente@gmail.com",
            };

            
            var response = await controller.PutClientAsync(1, client) as ObjectResult;
            var value = response.Value as IResponse;
            
            Assert.False(value.DidError);

        }
    }
}

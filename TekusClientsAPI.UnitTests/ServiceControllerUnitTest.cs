using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TekusClientsAPI.Controllers;
using TekusClientsAPI.Models;
using TekusClientsAPI.UnitTests;
using TekusClientsAPI.Utils;
using Xunit;

namespace TekusServicesAPI.UnitTests
{
    public class ServiceControllerUnitTest
    {
        [Fact]
        public async Task TestGetServicesAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetServicesAsync));
            var controller = new ServiceController(dbContext);

            var response = await controller.GetServicesAsync("") as ObjectResult;
            var value = response.Value as IPagedResponse<Service>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestGetServicesByIdAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetServicesByIdAsync));
            var controller = new ServiceController(dbContext);

            var response = await controller.GetServiceByIdAsync(1) as ObjectResult;
            var value = response.Value as ISingleResponse<Service>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestPostServiceAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestPostServiceAsync));
            var controller = new ServiceController(dbContext);

            var Service = new Service
            {
                Id = 0,
                Name = "Mi Service nuevo de prueba",
                Price = 20000,                
            };

            var response = await controller.PostServiceAsync(Service) as ObjectResult;
            var value = response.Value as ISingleResponse<Service>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestPutServiceAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestPutServiceAsync));
            var controller = new ServiceController(dbContext);

            var Service = new Service
            {
                Id = 1,
                Name = "Mi Servicee nuevo de prueba modificado",
                Price = 500.0
            };


            var response = await controller.PutServiceAsync(1, Service) as ObjectResult;
            var value = response.Value as IResponse;

            Assert.False(value.DidError);

        }
    }
}

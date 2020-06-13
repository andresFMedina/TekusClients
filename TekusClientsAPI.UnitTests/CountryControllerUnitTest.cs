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

namespace TekusCountriesAPI.UnitTests
{
    public class CountryControllerUnitTest
    {
        [Fact]
        public async Task TestGetCountriesAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetCountriesAsync));
            var controller = new CountryController(dbContext);

            var response = await controller.GetCountriesAsync("") as ObjectResult;
            var value = response.Value as IPagedResponse<Country>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestGetCountriesByIdAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestGetCountriesByIdAsync));
            var controller = new CountryController(dbContext);

            var response = await controller.GetCountryByIdAsync(1) as ObjectResult;
            var value = response.Value as ISingleResponse<Country>;

            Assert.False(value.DidError);

        }

        [Fact]
        public async Task TestPostCountryAsync()
        {
            var dbContext = DbContextMock.GetDbContext(nameof(TestPostCountryAsync));
            var controller = new CountryController(dbContext);

            var country = new Country
            {                
                Name = "USA"
                
            };

            var response = await controller.PostCountryAsync(country) as ObjectResult;
            var value = response.Value as ISingleResponse<Country>;

            Assert.False(value.DidError);

        }

        
    }
}

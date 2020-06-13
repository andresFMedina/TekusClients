using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TekusClientsAPI.Models;
using Xunit;

namespace TekusClientsAPI.IntegrationTests
{
    public class CountryIntegrationTests : IClassFixture<TestFixture<Startup>>
    {
        private const string URL = "/api/Country";

        private readonly HttpClient _Country;
        public CountryIntegrationTests(TestFixture<Startup> fixture)
        {
            _Country = fixture.Client;
        }

        [Fact]
        public async Task TestGetCountriesAsync()
        {
            var response = await _Country.GetAsync(URL);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetCountryByIdAsync()
        {
            var request = URL + "/1";
            var response = await _Country.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestGetCountryByIdNotFoundAsync()
        {
            var request = URL + "/1000";
            var response = await _Country.GetAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestPostCountryOKAsync()
        {
            var request = new
            {
                Url = URL,
                Body = new Country
                {
                    Name = "Colombia",                    
                }
            };
            var response = await _Country.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();            

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostCountryBadRequestAsync()
        {
            var request = new
            {
                Url = URL,
                Body = new
                {
                    Naem = "Colombia"
                }
            };
            var response = await _Country.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        
    }
}

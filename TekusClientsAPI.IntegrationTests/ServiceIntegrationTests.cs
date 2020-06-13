using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TekusClientsAPI.Models;
using Xunit;

namespace TekusClientsAPI.IntegrationTests
{
    public class ServiceIntegrationTests : IClassFixture<TestFixture<Startup>>
    {
        private const string URL = "/api/Service";

        private readonly HttpClient _Service;
        public ServiceIntegrationTests(TestFixture<Startup> fixture)
        {
            _Service = fixture.Client;
        }

        [Fact]
        public async Task TestGetServicesAsync()
        {
            var response = await _Service.GetAsync(URL);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetServiceByIdAsync()
        {
            var request = URL + "/10";
            var response = await _Service.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestGetServiceByIdNotFoundAsync()
        {
            var request = URL + "/1000";
            var response = await _Service.GetAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestPostServiceOKAsync()
        {
            var request = new
            {
                Url = URL,
                Body = new Service
                {
                    Name = "Nuevo Service",
                    Price = 320,
                    ProviderId = 1
                }
            };
            var response = await _Service.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));            
            var value = await response.Content.ReadAsStringAsync();
            
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostServiceBadRequestAsync()
        {
            var request = new
            {
                Url = URL,
                Body = new
                {                    
                    ProviderId = 1
                }
            };
            var response = await _Service.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestPutServiceOKAsync()
        {
            var request = new
            {
                Url = URL + "/10",
                Body = new Service
                {
                    Name = "Nuevo Service modificado",
                    Price = 200.50
                }
            };
            var response = await _Service.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));


            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutServiceBadRequestAsync()
        {
            var request = new
            {
                Url = URL + "/10",
                Body = new
                {                    
                    ProviderId = 1,
                }
            };
            var response = await _Service.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestPutNotFoundServiceAsync()
        {
            var request = new
            {
                Url = URL + "/1000",
                Body = new Service
                {
                    Name = "Nuevo Servicee modificado",
                    Price = 200
                }
            };
            var response = await _Service.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

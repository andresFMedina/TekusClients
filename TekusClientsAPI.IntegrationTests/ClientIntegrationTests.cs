using TekusClientsAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;

namespace TekusClientsAPI.IntegrationTests
{
    public class ClientIntegrationTests : IClassFixture<TestFixture<Startup>>
    {
        private const string URL = "/api/Client";

        private readonly HttpClient _client;
        public ClientIntegrationTests(TestFixture<Startup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task TestGetClientsAsync()
        {
            var response = await _client.GetAsync(URL);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetClientByIdAsync()
        {
            var request = URL + "/1";
            var response = await _client.GetAsync(request);

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestGetClientByIdNotFoundAsync()
        {
            var request = URL + "/1000";
            var response = await _client.GetAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task TestPostClientAsync()
        {
            var request = new
            { 
                Url = URL,
                Body = new Client
                {
                    Name = "Nuevo Cliente",
                    Email = "nuevo_cliente@gmail.com",
                    Nit = "8001239-8"
                }
            };
            var response = await _client.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostClientBadRequestAsync()
        {
            var request = new
            {
                Url = URL,
                Body = new 
                {
                    Name = "Nuevo Cliente",
                    Email = "nuevo_cliente@gmail.com",                    
                }
            };
            var response = await _client.PostAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestPutClientAsync()
        {
            var request = new
            {
                Url = URL + "/1",
                Body = new Client
                {
                    Name = "Nuevo Cliente modificado",
                    Email = "nuevo_cliente@gmail.com",
                    Nit = "8001239-8"
                }
            };
            var response = await _client.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));
            

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestPutClientBadRequestAsync()
        {
            var request = new
            {
                Url = URL + "/1",
                Body = new
                {
                    Name = "Nuevo Cliente modificado",
                    Email = "nuevo_cliente@gmail.com",                    
                }
            };
            var response = await _client.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestPutNotFoundClientAsync()
        {
            var request = new
            {
                Url = URL + "/1000",
                Body = new Client
                {
                    Name = "Nuevo Cliente modificado",
                    Email = "nuevo_cliente@gmail.com",
                    Nit = "8001239-8"
                }
            };
            var response = await _client.PutAsync(request.Url,
                ContentHelper.GetStringContent(request.Body));


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

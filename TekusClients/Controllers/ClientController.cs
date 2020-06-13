using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusClientsAPI.Infrastructure;
using TekusClientsAPI.Models;
using TekusClientsAPI.Utils;

namespace TekusClientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly ClientsContext _context;

        public ClientController(ClientsContext context)
        {
            _context = context;
        }


        // GET: api/Client
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetClientsAsync(string filter, int page = 0, int pageSize = 15)
        {
            var response = new PagedResponse<Client>();

            try
            {

                List<Client> clients;
                long totalResults;

                if (!string.IsNullOrEmpty(filter))
                {
                    foreach (string item in filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        clients = await _context.Clients
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

                        totalResults = await _context.Clients
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .LongCountAsync();

                        response.CurrentFilter = filter;
                        response.CurrentPage = page;
                        response.RegisterPerPages = pageSize;
                        response.TotalRegister = totalResults;
                        response.TotalPages = (int)Math.Ceiling((double)response.TotalRegister / pageSize);
                        response.Model = clients;

                        return response.ToHttpResponse();
                    }
                    
                }

                clients = await _context.Clients.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                totalResults = await _context.Clients.LongCountAsync();

                response.CurrentFilter = filter;
                response.CurrentPage = page;
                response.RegisterPerPages = pageSize;
                response.TotalRegister = totalResults;
                response.TotalPages = (int)Math.Ceiling((double)response.TotalRegister / pageSize);
                response.Model = clients;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();


        }

        // GET api/Clients/5        
        [HttpGet("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        
        public async Task<IActionResult> GetClientByIdAsync(int id)
        {
            var response = new SingleResponse<Client>();

            try
            {
                var client = await _context.Clients.FindAsync(id);

                if (client == null)
                {
                    return NotFound();
                }
                response.Model = client;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }


        // POST: api/Client
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(((int)HttpStatusCode.InternalServerError))]
        public async Task<IActionResult> PostClientAsync([FromBody] Client client)
        {
            var response = new SingleResponse<Client>();

            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                response.Model = CreatedAtAction(nameof(GetClientByIdAsync), new { id = client.Id }, client).Value as Client;
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]        
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(((int)HttpStatusCode.InternalServerError))]
        public async Task<IActionResult> PutClientAsync(int id, [FromBody] Client client)
        {
            var response = new Response();

            try 
            {
                var clientSelected = await _context.Clients.FindAsync(id);
                var clientUpdated = client;
                
                if(clientSelected == null)
                {
                    return NotFound();
                }
                clientSelected.Name = clientUpdated.Name;
                clientSelected.Email = clientUpdated.Email;

                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();

        }

    }
}

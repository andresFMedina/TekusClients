using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekusClientsAPI.Infrastructure;
using TekusClientsAPI.Models;
using TekusClientsAPI.Utils;

namespace TekusClientsAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCountryController : ControllerBase
    {
        private readonly ClientsContext _context;

        public ServiceCountryController(ClientsContext context)
        {
            _context = context;
        }

        // POST: api/Serivce
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> PostServiceCountryAsync([FromBody] ServiceCountry serviceCountry)
        {
            var response = new Response();

            try
            {
                _context.ServiceCountries.Add(serviceCountry);
                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }            

            return response.ToHttpResponse();
        }
    }
}
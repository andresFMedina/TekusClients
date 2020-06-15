using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using TekusClientsAPI.Infrastructure;
using TekusClientsAPI.Utils;

namespace TekusClientsAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestoreController : ControllerBase
    {
        private readonly ClientsContext _context;

        public RestoreController(ClientsContext context)
        {
            _context = context;
        }

        // Delete: api/Serivce
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]        
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> RestoreDatabaseAsync()
        {
            var response = new Response();

            try
            {
                _context.Database.ExecuteSqlCommand("delete from clients;" +
                    "delete from services;" +
                    "delete from countries;" +
                    "delete from ServiceCountries;");
                                    
                
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
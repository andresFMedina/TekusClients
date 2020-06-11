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
    public class CountryServiceController : ControllerBase
    {
        private readonly ClientsContext _context;

        public CountryServiceController(ClientsContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCountryServices(string filter, int page = 0, int pageSize = 15)
        {
            var response = new PagedResponse<CountryService>();

            try
            {

                List<CountryService> countryServices;
                long totalResults;

                if (!string.IsNullOrEmpty(filter))
                {
                    foreach (string item in filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        countryServices = await _context.CountryServices
                            .Where(c => c.Country.ToLower().StartsWith(item))
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

                        totalResults = await _context.CountryServices
                            .Where(c => c.Country.ToLower().StartsWith(item))
                            .LongCountAsync();

                        response.CurrentFilter = filter;
                        response.CurrentPage = page;
                        response.RegisterPerPages = pageSize;
                        response.TotalRegister = totalResults;
                        response.TotalPages = (int)Math.Ceiling((double)response.TotalRegister / pageSize);
                        response.Model = countryServices;

                        return response.ToHttpResponse();
                    }

                }

                countryServices = _context.CountryServices.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                totalResults = await _context.CountryServices.LongCountAsync();

                response.CurrentFilter = filter;
                response.CurrentPage = page;
                response.RegisterPerPages = pageSize;
                response.TotalRegister = totalResults;
                response.TotalPages = (int)Math.Ceiling((double)response.TotalRegister / pageSize);
                response.Model = countryServices;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();


        }

        // GET api/Service/5        
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetCountryServiceById(int id)
        {
            var response = new SingleResponse<CountryService>();

            try
            {
                var countryService = await _context.CountryServices.FindAsync(id);

                if (countryService == null)
                {
                    return NotFound();
                }
                response.Model = countryService;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }


        // POST: api/CountryService
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(((int)HttpStatusCode.InternalServerError))]
        public async Task<IActionResult> PostCountryService([FromBody] CountryService countryService)
        {
            var response = new SingleResponse<CountryService>();

            try
            {
                _context.CountryServices.Add(countryService);
                await _context.SaveChangesAsync();
                response.Model = CreatedAtAction(nameof(GetCountryServiceById), new { id = countryService.Id }, countryService).Value as CountryService;
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
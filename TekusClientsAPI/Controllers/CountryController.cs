﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusClientsAPI.Infrastructure;
using TekusClientsAPI.Models;
using TekusClientsAPI.Utils;

namespace TekusClientsAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ClientsContext _context;

        public CountryController(ClientsContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetCountriesAsync(string filter, int page = 1, int pageSize = 15)
        {
            var response = new PagedResponse<Country>();

            try
            {

                List<Country> countries;
                long totalResults;

                if (!string.IsNullOrEmpty(filter))
                {
                    foreach (string item in filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        countries = await _context.Countries
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

                        totalResults = await _context.Countries
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .LongCountAsync();

                        response = await createResponsePaginated(filter, page, pageSize, totalResults, countries);                        
                    }
                    return response.ToHttpResponse();

                }

                countries = _context.Countries.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                totalResults = await _context.Countries.LongCountAsync();

                response = await createResponsePaginated(filter, page, pageSize, totalResults, countries);
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
        [EnableCors("AllowOrigin")]

        public async Task<IActionResult> GetCountryByIdAsync(int id)
        {
            var response = new SingleResponse<Country>();

            try
            {
                var country = await _context.Countries.FindAsync(id);

                if (country == null)
                {
                    return NotFound();
                }
                response.Model = country;
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
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> PostCountryAsync([FromBody] Country country)
        {
            var response = new SingleResponse<Country>();

            try
            {
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                response.Model = CreatedAtAction(nameof(GetCountryByIdAsync), new { id = country.Id }, country).Value as Country;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }

        private async Task<PagedResponse<Country>> createResponsePaginated(string filter, int page, int pageSize, long totalResults, List<Country> countries)
        {
            foreach(var country in countries)
            {
                country.ServiceCountries = await _context.ServiceCountries.Where(cs => cs.CountryId == country.Id).ToListAsync();
            }

            return new PagedResponse<Country>
            {
                CurrentFilter = filter,
                CurrentPage = page,
                RegisterPerPages = pageSize,
                TotalRegister = totalResults,
                TotalPages = (int)Math.Ceiling((double)totalResults / pageSize),
                Model = countries
            };
        }

    }
}
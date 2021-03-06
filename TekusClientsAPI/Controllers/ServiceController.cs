﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    public class ServiceController : ControllerBase
    {
        private readonly ClientsContext _context;

        public ServiceController(ClientsContext context)
        {
            _context = context;
        }
        // GET: api/Service
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> GetServicesAsync(string filter="", int page = 1, int pageSize = 15)
        {
            var response = new PagedResponse<Service>();

            try
            {

                List<Service> services;
                long totalResults;

                if (!string.IsNullOrEmpty(filter))
                {
                    foreach (string item in filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        services = await _context.Services
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

                        totalResults = await _context.Services
                            .Where(c => c.Name.ToLower().StartsWith(item))
                            .LongCountAsync();

                        response = await createResponsePaginated(filter, page, pageSize, totalResults, services);
                        return response.ToHttpResponse();
                    }

                }

                services = await _context.Services.Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                totalResults = await _context.Services.LongCountAsync();

                response = await createResponsePaginated(filter, page, pageSize, totalResults, services);
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();


        }

        // GET api/Services/5        
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]

        public async Task<IActionResult> GetServiceByIdAsync(int id)
        {
            var response = new SingleResponse<Service>();

            try
            {
                var service = await _context.Services.FindAsync(id);

                if (service == null)
                {
                    return NotFound();
                }
                response.Model = service;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }


        // POST: api/Serivce
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> PostServiceAsync([FromBody] Service service)
        {
            var response = new SingleResponse<Service>();

            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                response.Model = CreatedAtAction(nameof(GetServiceByIdAsync), new { id = service.Id }, service).Value as Service;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.ToString();
            }

            return response.ToHttpResponse();
        }

        // PUT: api/Service/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(((int)HttpStatusCode.InternalServerError))]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> PutServiceAsync(int id, [FromBody] Service service)
        {
            var response = new Response();

            try
            {
                var serviceSelected = await _context.Services.FindAsync(id);
                var serviceUpdated = service;

                if (serviceSelected == null)
                {
                    return NotFound();
                }
                serviceSelected.Name = serviceUpdated.Name;
                serviceSelected.Price = serviceUpdated.Price;

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();

        }

        private async Task<PagedResponse<Service>> createResponsePaginated(string filter, int page, int pageSize, long totalResults, List<Service> services)
        {
            foreach (var service in services)
            {
                service.ServiceCountries = await _context.ServiceCountries.Where(cs => cs.ServiceId == service.Id).ToListAsync();
            }

            return new PagedResponse<Service>
            {
                CurrentFilter = filter,
                CurrentPage = page,
                RegisterPerPages = pageSize,
                TotalRegister = totalResults,
                TotalPages = (int)Math.Ceiling((double)totalResults / pageSize),
                Model = services
            };
        }
    }
}
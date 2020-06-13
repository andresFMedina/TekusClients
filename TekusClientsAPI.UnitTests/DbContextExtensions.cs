using System;
using System.Collections.Generic;
using System.Text;
using TekusClientsAPI.Infrastructure;
using TekusClientsAPI.Models;

namespace TekusClientsAPI.UnitTests
{
    static class DbContextExtensions
    {
        public static void Seed(this ClientsContext context)
        {
            context.Clients.Add(new Client
            {
                Id = 1,
                Nit = "8004390-9",
                Name = "Importaciones Tekus S.A.S.",
                Email = "importaciones@tekus.com"
            });

            context.Clients.Add(new Client
            {
                Id = 2,
                Nit = "8003290-4",
                Name = "SpaceZ",
                Email = "contact@spacez.com"
            });

            context.Services.Add(new Service
            {
                Id = 1,
                Name = "Descarga espacial de contenidos",
                Price = 22.09,
                ProviderId = 1
            });

            context.Services.Add(new Service
            {
                Id = 2,
                Name = "Desaparicion Forzada de bytes",
                Price = 300.0,
                ProviderId = 1
            });

            context.Services.Add(new Service
            {
                Id = 3,
                Name = "Tour por Marte",
                Price = 250000000.0,
                ProviderId = 2
            });

            context.Countries.Add(new Country
            {
                Id = 1,
                Name = "Colombia",                
            });

            context.Countries.Add(new Country
            {
                Id = 2,
                Name = "Mexico",
            });

            context.Countries.Add(new Country
            {
                Id = 3,
                Name = "Peru",
            });

            context.ServiceCountries.Add(new ServiceCountry
            {
                CountryId = 1,
                ServiceId = 1
            
            });

            context.ServiceCountries.Add(new ServiceCountry
            {
                CountryId = 2,
                ServiceId = 1

            });
            context.ServiceCountries.Add(new ServiceCountry
            {
                CountryId = 2,
                ServiceId = 2

            });
            context.ServiceCountries.Add(new ServiceCountry
            {
                CountryId = 3,
                ServiceId = 3

            });

            context.SaveChanges();
        }
    }
}

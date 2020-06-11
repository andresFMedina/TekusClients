using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TekusClientsAPI.Models
{
    public class CountryService
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public int ServiceId { get; set; }
        public virtual  Service Service { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace TekusClientsAPI.Models
{
    public class ServiceCountry
    {
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
    }
}

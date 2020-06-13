using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TekusClientsAPI.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public virtual Client Provider { get; set; }

        public virtual ICollection<ServiceCountry> ServiceCountries { get; set; }

    }
}

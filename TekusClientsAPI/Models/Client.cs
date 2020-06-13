using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekusClientsAPI.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nit { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }   

    }
}

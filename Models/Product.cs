using System.ComponentModel.DataAnnotations;

namespace WebAPIClientServer.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        
        [Required]
        public string? Category { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string City { get; set; }
        [Required]  
        public string State { get; set; }
        [Required]  
        public string Country { get; set; }
        [Required]  
        public int Zip { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class AddLocationDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ZipCode must be number.")]
        public int Zip { get; set; }
    }
}

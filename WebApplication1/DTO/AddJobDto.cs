using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class AddJobDto
    {
        [Required]   
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "LocationId must be numeric.")]
        public int LocationId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}

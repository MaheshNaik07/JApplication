using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class JobListRequest
    {
        public string q { get; set; }
        [Required]
        public int PageNo { get; set; } 

        [Required]
        public int PageSize { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
    }
}

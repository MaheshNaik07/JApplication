using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class AddDepartmentDto
    {
        [Required]
        public string Title { get; set; }
    }
}

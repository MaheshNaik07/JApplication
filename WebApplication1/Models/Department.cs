using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}

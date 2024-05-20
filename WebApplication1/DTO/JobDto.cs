using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime PostedDate { get; set; }
        public string Code { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
    }
}

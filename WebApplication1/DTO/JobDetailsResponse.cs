namespace WebApplication1.DTO
{
    public class JobDetailsResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }
        public DepartmentDto Department { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}

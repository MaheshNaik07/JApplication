namespace WebApplication1.DTO
{
    public class JobListResponse
    {
        public int Total { get; set; }
        public List<JobData> Data { get; set; }
    }

    public class JobData
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}

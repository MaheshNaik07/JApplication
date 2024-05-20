using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IJobRepository
    {
        Task<JobDetailsResponse?> GetByIdAsync(int id);
        Task<Job> CreateAsync(Job job);
        Task<Job?> UpdateAsync(int id, Job job);
        Task<JobListResponse?> GetListAsync(JobListRequest jobListRequest);
    }
}

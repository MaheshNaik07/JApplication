using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication1.Repositories
{
    public class JobRepository:IJobRepository
    {
        private readonly AppDBContext appDBContext;

        public JobRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<JobDetailsResponse?> GetByIdAsync(int id)
        {
            var jobDetails = await appDBContext.Jobs
            .Include(j => j.Location)
            .Include(j => j.Department)
            .FirstOrDefaultAsync(j => j.Id == id);
            if (jobDetails == null)
            {
                return null;
            }
            var JobDetailsResponse = new JobDetailsResponse
            {
                Id = jobDetails.Id,
                Code = jobDetails.Code,
                Title = jobDetails.Title,
                Description = jobDetails.Description,
                PostedDate = jobDetails.PostedDate,
                ClosingDate = jobDetails.ClosingDate,
                Location = new LocationDto
                {
                    Id = jobDetails.Location.Id,
                    Title = jobDetails.Location.Title,
                    City = jobDetails.Location.City,
                    State = jobDetails.Location.State,
                    Country = jobDetails.Location.Country,
                    Zip = jobDetails.Location.Zip
                },
                Department = new DepartmentDto
                {
                    Id = jobDetails.Department.Id,
                    Title = jobDetails.Department.Title
                }
            };
            return JobDetailsResponse;
        }

        public async Task<Job> CreateAsync(Job job)
        {
            var lastJob = appDBContext.Jobs.OrderByDescending(j => j.Id).FirstOrDefault();
            int jobId = (lastJob?.Id ?? 0) + 1;
            job.Code= $"Job-{jobId:D2}";
            await appDBContext.Jobs.AddAsync(job);
            await appDBContext.SaveChangesAsync();
            return job;
        }


        public async Task<Job?> UpdateAsync(int id, Job job)
        {
            var jobModel = await appDBContext.Jobs.FindAsync(id);
            if (jobModel == null)
            {
                return null;
            }
            jobModel.Title = job.Title;
            jobModel.Description = job.Description;
            jobModel.ClosingDate = job.ClosingDate;
            jobModel.LocationId= job.LocationId;
            jobModel.DepartmentId= job.DepartmentId;    
            await appDBContext.SaveChangesAsync();
            return jobModel;
        }

        public async Task<JobListResponse?> GetListAsync(JobListRequest jobListRequest)
        {
            if (jobListRequest.PageNo < 1) jobListRequest.PageNo = 1;
            if (jobListRequest.PageSize < 1) jobListRequest.PageSize = 10;
            var JobData =appDBContext.Jobs.Include(j => j.Department).Include(j => j.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(jobListRequest.q))
            {
                JobData = JobData.Where(j=>j.Title.Contains(jobListRequest.q) || j.Description.Contains(jobListRequest.q));
            }
            if (jobListRequest.LocationId.HasValue)
            {
                JobData = JobData.Where(j => j.LocationId==jobListRequest.LocationId.Value);
            }
            if (jobListRequest.DepartmentId.HasValue)
            {
                JobData = JobData.Where(j => j.DepartmentId == jobListRequest.DepartmentId.Value);
            }
            var total = await JobData.CountAsync();
            var jobListResult = await JobData
           .Skip((jobListRequest.PageNo - 1) * jobListRequest.PageSize)
           .Take(jobListRequest.PageSize)
           .Select(j => new JobData
           {
               Id = j.Id,
               Code = j.Code,
               Title = j.Title,
               Location = j.Location.Title,
               Department = j.Department.Title,
               PostedDate = j.PostedDate,
               ClosingDate = j.ClosingDate
           })
           .ToListAsync();
            if (jobListResult == null)
            {
                return null;
            }
            var filteredJobList = new JobListResponse
            {
                Total = total,
                Data = jobListResult
            };
            return filteredJobList;
        }
    }

}

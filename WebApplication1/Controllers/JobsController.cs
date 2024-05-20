using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/Jobs/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var job = await jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/Jobs")]
        public async Task<IActionResult> Create([FromBody] AddJobDto job)
        {
            var jobModel = new Job
            {
                Title= job.Title,
                Description= job.Description,
                LocationId= job.LocationId,
                DepartmentId= job.DepartmentId,
                ClosingDate= job.ClosingDate,
                PostedDate=DateTime.Now
            };
            jobModel = await jobRepository.CreateAsync(jobModel);
            return Created("", new { id = jobModel.Id });
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/Jobs/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AddJobDto updateJobDto)
        {
            var existingJobModel = new Job
            {
                Title = updateJobDto.Title,
                Description = updateJobDto.Description,
                LocationId = updateJobDto.LocationId,
                DepartmentId = updateJobDto.DepartmentId,
                ClosingDate = updateJobDto.ClosingDate
            };
            var jobModel = await jobRepository.UpdateAsync(id, existingJobModel);
            if (jobModel == null)
            {
                return NotFound();
            }
            var jobDto = new JobDto
            {
                Id = jobModel.Id,
                Title = jobModel.Title,
                Description= jobModel.Description,
                Code = jobModel.Code,
                DepartmentId= jobModel.DepartmentId,
                LocationId= jobModel.LocationId,
                ClosingDate=jobModel.ClosingDate ,
                PostedDate= jobModel.PostedDate
            };
            return Ok(jobDto);
        }

        //GetListAsync
        [HttpPost]
        [Authorize]
        [Route("/api/v1/Jobs/List")]
        public async Task<IActionResult> List(JobListRequest jobListRequest)
        {
            var jobList=await jobRepository.GetListAsync(jobListRequest);
            if (jobList == null)
            {
                return NotFound();
            }
            return Ok(jobList);
        }


    }


}

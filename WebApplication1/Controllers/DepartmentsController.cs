using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }



        [Authorize]
        [HttpGet]
        [Route("/api/v1/Departments")]
        public async Task<IActionResult> GetAll()
        {
            var department = await departmentRepository.GetAllAsync();
            return Ok(department);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/Departments/{id:int}")]
        //[Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var department =await departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/Departments/")]
        public async Task<IActionResult> Create([FromBody] AddDepartmentDto department)
        {
            var departmentModel = new Department
            {
                Title = department.Title
            };
            departmentModel=await departmentRepository.CreateAsync(departmentModel);
            var departmentDto = new DepartmentDto
            {
                Id= departmentModel.Id,
                Title = departmentModel.Title
            };
            return CreatedAtAction(nameof(GetById), new {id= departmentModel.Id},departmentDto);
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/Departments/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AddDepartmentDto updateDepartmentDto)
        {
            var existingDepartmentModel = new Department
            {
                Title = updateDepartmentDto.Title
            };
            var departmentModel = await departmentRepository.UpdateAsync(id, existingDepartmentModel);
            if (departmentModel == null)
            {
                return NotFound();
            }
            var departmentDto = new DepartmentDto
            {
                Id = departmentModel.Id,
                Title = departmentModel.Title
            };
            return Ok(departmentDto);
        }      
    }
}

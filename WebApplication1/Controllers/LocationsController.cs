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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [Authorize]
        [Route("/api/v1/Locations")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await locationRepository.GetAllAsync();
            return Ok(locations);
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/Locations/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var location = await locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/Locations/")]
        public async Task<IActionResult> Create([FromBody] AddLocationDto location)
        {
            var locationModel = new Location
            {
                Title = location.Title,
                City = location.City,
                State = location.State,
                Country = location.Country,
                Zip=location.Zip
            };
            locationModel = await locationRepository.CreateAsync(locationModel);
            var locationDto = new LocationDto
            {
                Id = locationModel.Id,
                Title = locationModel.Title,
                City = locationModel.City,
                State = locationModel.State,
                Country = locationModel.Country,
                Zip = locationModel.Zip
            };
            return CreatedAtAction(nameof(GetById), new { id = locationModel.Id }, locationDto);
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/Locations/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AddLocationDto updateLocationDto)
        {
            var existingLocationModel = new Location
            {
                Title = updateLocationDto.Title,
                City = updateLocationDto.City,
                State = updateLocationDto.State,
                Country = updateLocationDto.Country,
                Zip = updateLocationDto.Zip
            };
            var locationModel = await locationRepository.UpdateAsync(id, existingLocationModel);
            if (locationModel == null)
            {
                return NotFound();
            }
            var locationDto = new LocationDto
            {
                Id = locationModel.Id,
                Title = locationModel.Title,
                City = locationModel.City,
                State = locationModel.State,
                Country = locationModel.Country,
                Zip = locationModel.Zip
            };
            return Ok(locationDto);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickYourTrip.API.DTOs;
using PickYourTrip.DataAccess.Repositories;
using PickYourTrip.Domain;
using System.Threading.Tasks;

namespace PickYourTrip.API.Controllers
{
    [ApiController]
    [Route("api/admin/destinations")]
    [Authorize(Roles = "Admin")] // Secure this entire controller for Admins only
    public class AdminDestinationsController : ControllerBase
    {
        private readonly DestinationRepository _destinationRepository;

        public AdminDestinationsController(DestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddDestination([FromBody] DestinationDto destinationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var destination = new Destination
            {
                Name = destinationDto.Name,
                Country = destinationDto.Country,
                ImageUrl = destinationDto.ImageUrl,
                Description = destinationDto.Description
            };

            await _destinationRepository.AddAsync(destination);
            return CreatedAtAction(nameof(GetDestinationById), new { id = destination.Id }, destination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDestination(int id, [FromBody] DestinationDto destinationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingDestination = await _destinationRepository.GetByIdAsync(id);
            if (existingDestination == null) return NotFound();

            existingDestination.Name = destinationDto.Name;
            existingDestination.Country = destinationDto.Country;
            existingDestination.ImageUrl = destinationDto.ImageUrl;
            existingDestination.Description = destinationDto.Description;

            await _destinationRepository.UpdateAsync(existingDestination);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _destinationRepository.GetByIdAsync(id);
            if (destination == null) return NotFound();

            await _destinationRepository.DeleteAsync(id);
            return NoContent();
        }

        // Helper to get a destination by ID for the CreatedAtAction response
        [HttpGet("{id}", Name = "GetDestinationById")]
        public async Task<ActionResult<Destination>> GetDestinationById(int id)
        {
            var destination = await _destinationRepository.GetByIdAsync(id);
            if (destination == null) return NotFound();
            return Ok(destination);
        }
    }
}
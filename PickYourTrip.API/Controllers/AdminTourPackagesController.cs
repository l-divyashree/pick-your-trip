using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickYourTrip.DataAccess.Repositories;
using PickYourTrip.Domain;
using System.Threading.Tasks;
using PickYourTrip.API.DTOs;
using System;

namespace PickYourTrip.API.Controllers
{
    [ApiController]
    [Route("api/admin/tourpackages")]
    [Authorize(Roles = "Admin")]
    public class AdminTourPackagesController : ControllerBase
    {
        private readonly TourPackageRepository _tourPackageRepository;

        public AdminTourPackagesController(TourPackageRepository tourPackageRepository)
        {
            _tourPackageRepository = tourPackageRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTourPackage([FromBody] TourPackageDto newPackageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newTourPackage = new TourPackage
            {
                Name = newPackageDto.Name,
                DurationDays = newPackageDto.DurationDays,
                Price = newPackageDto.Price,
                ImageUrl = newPackageDto.ImageUrl,
                DestinationId = newPackageDto.DestinationId,
                // NEW: Save the new fields
                Description = newPackageDto.Description,
                Itinerary = newPackageDto.Itinerary
            };

            await _tourPackageRepository.AddAsync(newTourPackage);
            return CreatedAtAction(nameof(GetById), new { id = newTourPackage.Id }, newTourPackage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTourPackage(int id, [FromBody] TourPackageDto packageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingPackage = await _tourPackageRepository.GetByIdAsync(id);
            if (existingPackage == null) return NotFound("Package not found.");

            existingPackage.Name = packageDto.Name;
            existingPackage.ImageUrl = packageDto.ImageUrl;
            existingPackage.DurationDays = packageDto.DurationDays;
            existingPackage.Price = packageDto.Price;
            existingPackage.DestinationId = packageDto.DestinationId;
            // NEW: Update the new fields
            existingPackage.Description = packageDto.Description;
            existingPackage.Itinerary = packageDto.Itinerary;

            await _tourPackageRepository.UpdateAsync(existingPackage);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourPackage(int id)
        {
            var packageToDelete = await _tourPackageRepository.GetByIdAsync(id);
            if (packageToDelete == null) return NotFound();
            await _tourPackageRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourPackage>> GetById(int id)
        {
            var package = await _tourPackageRepository.GetByIdAsync(id);
            if (package == null) return NotFound();
            return Ok(package);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickYourTrip.DataAccess.Repositories;
using PickYourTrip.Domain;
using System;
using System.Threading.Tasks;

namespace PickYourTrip.API.Controllers
{
    // This is the API controller for managing tour packages.
    // It handles the HTTP requests for adding, updating, and deleting tour packages.
    [ApiController]
    [Route("api/[controller]")]
    public class TourPackagesController : ControllerBase
    {
        private readonly TourPackageRepository _repository;

        public TourPackagesController(TourPackageRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TourPackages
        // This endpoint retrieves all tour packages.
        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _repository.GetAllAsync();
            return Ok(packages);
        }

        // GET: api/TourPackages/5
        // This endpoint retrieves a single tour package by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var package = await _repository.GetByIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        // POST: api/TourPackages
        // This endpoint adds a new tour package.
        // It's decorated with [Authorize(Roles = "Admin")] to restrict access to administrators only.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPackage([FromBody] TourPackage package)
        {
            await _repository.AddAsync(package);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.Id }, package);
        }

        // PUT: api/TourPackages/5
        // This endpoint updates an existing tour package.
        // It also requires the user to have an "Admin" role.
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] TourPackage package)
        {
            // Check if the package object from the request body is null.
            if (package == null)
            {
                return BadRequest("Package data is missing from the request body.");
            }

            // Check for ID mismatch between the URL and the request body.
            if (id != package.Id)
            {
                return BadRequest("Package ID mismatch.");
            }

            // Check if the package exists in the database.
            var existingPackage = await _repository.GetByIdAsync(id);
            if (existingPackage == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateAsync(package);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception to the console for detailed debugging.
                Console.WriteLine($"An error occurred while updating the tour package: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/TourPackages/5
        // This endpoint deletes a tour package by its ID.
        // This operation is also restricted to administrators.
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

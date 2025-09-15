using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickYourTrip.DataAccess.Repositories;
using PickYourTrip.Domain;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PickYourTrip.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;
        private readonly TourPackageRepository _tourPackageRepository;

        public BookingsController(BookingRepository bookingRepository, TourPackageRepository tourPackageRepository)
        {
            _bookingRepository = bookingRepository;
            _tourPackageRepository = tourPackageRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            var result = bookings.Select(b => new
            {
                b.Id,
                b.BookingDate,
                b.NumberOfTravelers,
                b.TotalPrice,
                User = new { b.User.Name, b.User.Email },
                TourPackage = new { b.TourPackage.Id, b.TourPackage.Name } // Also added Id here for consistency
            });
            return Ok(result);
        }

        [HttpGet("mybookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized("User ID invalid.");

            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            var result = bookings.Select(b => new
            {
                b.Id,
                b.BookingDate,
                b.NumberOfTravelers,
                b.TotalPrice,
                TourPackage = new
                {
                    // =================================================================
                    // THIS IS THE FIX: We are now including the package ID.
                    Id = b.TourPackage.Id,
                    // =================================================================
                    Name = b.TourPackage.Name
                }
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] BookingDto bookingDto)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId)) return Unauthorized();

            var tourPackage = await _tourPackageRepository.GetByIdAsync(bookingDto.TourPackageId);
            if (tourPackage == null) return BadRequest("Tour package not found.");

            var booking = new Booking
            {
                UserId = userId,
                TourPackageId = bookingDto.TourPackageId,
                BookingDate = DateTime.UtcNow,
                NumberOfTravelers = bookingDto.NumberOfTravelers,
                TotalPrice = bookingDto.NumberOfTravelers * tourPackage.Price
            };

            await _bookingRepository.AddBookingAsync(booking);
            return Ok(new { Message = "Booking created successfully!" });
        }
    }

    public class BookingDto
    {
        [Required]
        public int TourPackageId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfTravelers { get; set; }
    }
}
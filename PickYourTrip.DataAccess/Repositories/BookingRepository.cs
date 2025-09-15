using Microsoft.EntityFrameworkCore;
using PickYourTrip.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickYourTrip.DataAccess.Repositories
{
    public class BookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
        }

        // This method for admins is unchanged
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _dbContext.Bookings
                .Include(b => b.User)
                .Include(b => b.TourPackage)
                .ToListAsync();
        }

        // NEW: Method to get bookings for a specific user ID
        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.TourPackage) // We need the package name
                .ToListAsync();
        }
    }
}
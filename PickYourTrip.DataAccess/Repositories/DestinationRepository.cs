using Microsoft.EntityFrameworkCore;
using PickYourTrip.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickYourTrip.DataAccess.Repositories
{
    public class DestinationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DestinationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _dbContext.Destinations.ToListAsync();
        }

        // NEW: Get a single destination by its ID
        public async Task<Destination> GetByIdAsync(int id)
        {
            return await _dbContext.Destinations.FindAsync(id);
        }

        // NEW: Add a new destination
        public async Task AddAsync(Destination destination)
        {
            await _dbContext.Destinations.AddAsync(destination);
            await _dbContext.SaveChangesAsync();
        }

        // NEW: Update an existing destination
        public async Task UpdateAsync(Destination destination)
        {
            _dbContext.Destinations.Update(destination);
            await _dbContext.SaveChangesAsync();
        }

        // NEW: Delete a destination by its ID
        public async Task DeleteAsync(int id)
        {
            var destination = await GetByIdAsync(id);
            if (destination != null)
            {
                _dbContext.Destinations.Remove(destination);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PickYourTrip.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickYourTrip.DataAccess.Repositories
{
    public class TourPackageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TourPackageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TourPackage>> GetAllAsync()
        {
            return await _dbContext.Packages
                                   .Include(p => p.Destination)
                                   .ToListAsync();
        }

        public async Task<TourPackage> GetByIdAsync(int id)
        {
            return await _dbContext.Packages
                                   .Include(p => p.Destination)
                                   .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Method for adding a tour package
        public async Task AddAsync(TourPackage package)
        {
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        // Corrected method for updating a tour package
        public async Task UpdateAsync(TourPackage package)
        {
            // First, find the existing entity in the database
            var existingPackage = await _dbContext.Packages
                                                  .FirstOrDefaultAsync(p => p.Id == package.Id);

            if (existingPackage != null)
            {
                // Update the properties of the existing entity with the new values.
                // This ensures the entity is being tracked by the DbContext.
                existingPackage.Name = package.Name;
                existingPackage.Price = package.Price;
                existingPackage.DurationDays = package.DurationDays;
                existingPackage.DestinationId = package.DestinationId;

                // You can add other properties to update here as needed.

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();
            }
        }

        // Method for deleting a tour package
        public async Task DeleteAsync(int id)
        {
            var package = await _dbContext.Packages.FindAsync(id);
            if (package != null)
            {
                _dbContext.Packages.Remove(package);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PickYourTrip.Domain;

namespace PickYourTrip.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<TourPackage> Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        //public DbSet<Review> Reviews { get; set; }
    }
}
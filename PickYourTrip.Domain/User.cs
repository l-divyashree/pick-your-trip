using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PickYourTrip.Domain
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        // This is the crucial navigation property for the relationship with Bookings
        public ICollection<Booking> Bookings { get; set; }
    }
}

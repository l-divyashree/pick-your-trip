using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickYourTrip.Domain
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        public int NumberOfTravelers { get; set; }

        public decimal TotalPrice { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        // Foreign key to TourPackage
        public int TourPackageId { get; set; }
        [ForeignKey("TourPackageId")]
        public TourPackage TourPackage { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickYourTrip.Domain
{
    public class TourPackage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        // NEW: Add fields for detailed information
        public string Description { get; set; }
        public string Itinerary { get; set; } // We can store a simple day-by-day text here

        // Foreign key to Destination
        public int DestinationId { get; set; }
        [ForeignKey("DestinationId")]
        public Destination Destination { get; set; }
    }
}
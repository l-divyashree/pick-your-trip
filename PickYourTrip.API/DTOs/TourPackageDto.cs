namespace PickYourTrip.API.DTOs
{
    public class TourPackageDto
    {
        public string Name { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        public string ImageUrl { get; set; }

        // NEW: Add the new fields to the DTO
        public string Description { get; set; }
        public string Itinerary { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PickYourTrip.API.DTOs
{
    public class DestinationDto
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE_Part2.Models
{
    public class Venue
    {
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Venue name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Venue Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 200 characters")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100000, ErrorMessage = "Capacity must be between 1 and 100,000")]
        public int Capacity { get; set; }

        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid image URL")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Venue";

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

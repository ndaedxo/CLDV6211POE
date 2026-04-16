using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE_Part2.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Event Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Image URL")]
        [Url(ErrorMessage = "Please enter a valid image URL")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Event";

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

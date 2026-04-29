using System.ComponentModel.DataAnnotations;

namespace CLDV6211POE_Part2.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Event is required")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Venue is required")]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Booking date is required")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        public Event Event { get; set; } = null!;
        public Venue Venue { get; set; } = null!;
    }
}

namespace CLDV6211POE_Part1.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public DateTime BookingDate { get; set; }

        public Event Event { get; set; } = null!;
        public Venue Venue { get; set; } = null!;
    }
}

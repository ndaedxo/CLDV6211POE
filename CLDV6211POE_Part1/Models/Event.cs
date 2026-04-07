namespace CLDV6211POE_Part1.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Event";

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

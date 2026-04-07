namespace CLDV6211POE_Part1.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Venue";

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

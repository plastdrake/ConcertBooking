namespace ConcertBooking.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; } = 0!;
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public DateTime PerformanceDate { get; set; }
        public string Venue { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ConcertTitle { get; set; } = null!;
    }
}
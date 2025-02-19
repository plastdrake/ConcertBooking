namespace ConcertBooking.DTO
{
    public class PerformanceDTO
    {
        public int ID { get; set; } = 0!;
        public DateTime PerformanceDate { get; set; }
        public string Venue { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ConcertTitle { get; set; } = null!;
    }
}
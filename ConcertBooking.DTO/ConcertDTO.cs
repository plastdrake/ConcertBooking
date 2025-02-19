namespace ConcertBooking.DTO
{
    public class ConcertDTO
    {
        public int ConcertId { get; set; } = 0!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Initialize as an empty list by default to avoid null issues
        public IEnumerable<PerformanceDTO> Performances { get; set; } = new List<PerformanceDTO>();
    }
}
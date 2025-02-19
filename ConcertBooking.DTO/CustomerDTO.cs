namespace ConcertBooking.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; } = 0!;
        public string CustomerFirstName { get; set; } = null!;
        public string CustomerLastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}

namespace ConcertBooking.Data.DTO
{
    public class CustomerUpdateDTO
    {
        public int Id { get; set; } = 0!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}


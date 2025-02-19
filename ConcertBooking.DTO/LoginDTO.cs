using System.ComponentModel.DataAnnotations;

namespace ConcertBooking.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(30)]
        public string Password { get; set; } = null!;
    }
}
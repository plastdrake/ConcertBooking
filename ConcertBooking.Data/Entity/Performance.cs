using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Entity
{
    public class Performance
    {
        [Key]
        public required int Id { get; set; }
        [Required]
        public required DateTime PerformanceDateAndTime { get; set; }
        [Required]
        public required string Venue { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string Country { get; set; }
        [ForeignKey(nameof(Concert))]
        public required int ConcertId { get; set; }

        //Navigation properties
        public Concert? Concert { get; set; }

        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();

    }
}
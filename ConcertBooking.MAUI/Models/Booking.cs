using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        // Customer details
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int CustomerId { get; set; }

        // Performance & concert details
        public DateTime PerformanceDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ConcertTitle { get; set; } = string.Empty;
        public int PerformanceId { get; set; }
    }
}

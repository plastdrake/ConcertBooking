using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Models
{
    public class Performance
    {
        public int Id { get; set; } = 0!;
        public DateTime PerformanceDateAndTime { get; set; }
        public string Venue { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        //Navigation properties
        public Concert? Concert { get; set; }
    }
}

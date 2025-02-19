using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Models
{
    public class Concert
    {
        public int Id { get; set; } = 0!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        //Navigation properties
        public ICollection<Performance>? Performances { get; set; }
    }
}

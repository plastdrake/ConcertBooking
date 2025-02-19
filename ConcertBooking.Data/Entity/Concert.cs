using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Entity
{
    public class Concert
    {
        [Key]
        public required int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [Required]
        [StringLength(500)]
        public required string Description { get; set; }

        //Navigation properties
        public ICollection<Performance> Performances = new List<Performance>();
    }
}

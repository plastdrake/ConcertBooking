using System.ComponentModel.DataAnnotations;

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

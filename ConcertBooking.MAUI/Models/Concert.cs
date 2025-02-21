using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Models
{
    public class Concert : ObservableObject
    {
        public int Id { get; set; } = 0!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        //Navigation properties
        public ObservableCollection<Performance> Performances { get; set; } = new();
    }
}

using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface IBookingService
    {
        Task<ObservableCollection<Booking>?> GetBookingsAsync();
        Task SaveBookingAsync(Booking item, bool isNewItem);
        Task DeleteBookingAsync(Booking item);
    }
}
using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface IBookingService
    {
        Task<List<Booking>?> GetBookingsAsync();
        Task SaveBookingAsync(Booking item, bool isNewItem);
        Task DeleteBookingAsync(Booking item);
    }
}
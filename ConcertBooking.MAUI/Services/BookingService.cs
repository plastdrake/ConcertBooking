using System;
using System.Collections.Generic;
using ConcertBooking.MAUI.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public class BookingService : IBookingService
    {
        IRestService _restService;
        public BookingService(IRestService service)
        {
            _restService = service;
        }
        public Task<ObservableCollection<Booking>?> GetBookingsAsync()
        {
            return _restService.RefreshBookingDataAsync();
        }
        public Task SaveBookingAsync(Booking booking, bool isNewBooking = false)
        {
            return _restService.SaveBookingAsync(booking, isNewBooking);
        }
        public Task DeleteBookingAsync(Booking booking)
        {
            return _restService.DeleteBookingAsync(booking.BookingId);
        }
    }
}

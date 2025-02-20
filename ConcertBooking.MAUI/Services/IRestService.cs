using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface IRestService
    {
        Task<ObservableCollection<Booking>?> RefreshBookingDataAsync();
        Task SaveBookingAsync(Booking booking, bool isNewBooking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<bool> CreateBookingAsync(BookingCreateDTO bookingDto);
        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task<Booking?> GetBookingByBookingIdAsync(int bookingId);

        Task<bool> RegisterCustomerDataAsync(Customer customer);
        Task<Customer?> LoginAsync(string email, string password);
        Task<Customer?> GetProfileAsync(int customerId);

        Task<ObservableCollection<Concert>?> RefreshConcertDataAsync();
        Task<Concert?> GetConcertByIdAsync(int id);

        Task<ObservableCollection<Performance>> GetPerformancesAsync(int concertId, int customerId);
    }
}

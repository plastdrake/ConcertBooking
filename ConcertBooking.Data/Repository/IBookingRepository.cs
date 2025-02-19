using ConcertBooking.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetAllBookingDetailsByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllBookingDetailsAsync();
        Task<Booking?> FindByCustomerAndPerformanceAsync(int customerId, int performanceId);
        Task<IEnumerable<Booking>> GetAllBookingsByCustomerIdAsync(int customerId);
        void Delete(int id);
    }
}
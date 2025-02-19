using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IBookingRepository Bookings { get; private set; }
        public IConcertRepository Concerts { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IPerformanceRepository Performances { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Bookings = new BookingRepository(context);
            Concerts = new ConcertRepository(context);
            Customers = new CustomerRepository(context);
            Performances = new PerformanceRepository(context);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}

﻿using ConcertBooking.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public BookingRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Booking>> GetAllBookingDetailsByIdAsync(int id)
        {
            return await DbContext.Bookings
                .Include(c => c.Customer)
                .Include(p => p.Performance)
                .ThenInclude(c => c.Concert)
                .Where(b => b.Id == id)
                .ToListAsync();
        }

        public void Delete(int id)
        {
            Booking? booking = DbContext.Bookings.Find(id);
            if (booking is not null)
                DbContext.Bookings.Remove(booking);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingDetailsAsync()
        {
            return await DbContext.Bookings
                .Include(c => c.Customer)
                .Include(p => p.Performance)
                .ThenInclude(c => c.Concert)
                .ToListAsync();
        }

        public async Task<Booking?> FindByCustomerAndPerformanceAsync(int customerId, int performanceId)
        {
            return await DbContext.Bookings.FirstOrDefaultAsync(b => b.CustomerId == customerId && b.PerformanceId == performanceId);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsByCustomerIdAsync(int customerId)
        {
            return await DbContext.Bookings
                .Include(c => c.Customer)
                .Include(p => p.Performance)
                .ThenInclude(c => c.Concert)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }
    }
}

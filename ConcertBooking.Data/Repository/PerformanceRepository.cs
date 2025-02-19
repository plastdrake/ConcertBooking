using ConcertBooking.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public class PerformanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public PerformanceRepository(ApplicationDbContext context)
            : base(context) { }


        public async Task<IEnumerable<Performance>> GetAllPerformancesAsync()
        {
            return await DbContext.Performances
                .Include(p => p.Concert)
                .Include(p => p.Bookings)
                .ToListAsync();
        }

        public async Task<IEnumerable<Performance>> GetAvailablePerformancesForCustomerAsync(int concertId, int customerId)
        {
            return await DbContext.Performances
                            .Where(p => p.ConcertId == concertId &&
                                        !p.Bookings.Any(b => b.CustomerId == customerId))
                            .Include(p => p.Concert)
                            .Include(p => p.Bookings)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Performance>> GetPerformancesByConcertIdAsync(int concertId)
        {
            return await DbContext.Performances
                .Where(p => p.ConcertId == concertId)
                .Include(p => p.Concert)
                .Include(p => p.Bookings)
                .ToListAsync();
        }

        public async Task<Performance?> GetPerformanceByIdAsync(int id)
        {
            return await DbContext.Performances
                .Include(p => p.Concert)
                .Include(p => p.Bookings)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

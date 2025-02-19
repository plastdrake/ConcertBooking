using ConcertBooking.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public ConcertRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<IEnumerable<Concert>> GetAllConcertsAsync()
        {
            return await DbContext.Concerts.Include(c => c.Performances).ToListAsync();
        }

        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            var concert = await DbContext.Concerts.Include(c => c.Performances).ThenInclude(p => p.Bookings).FirstOrDefaultAsync(c => c.Id == id);

            if (concert == null)
            {
                throw new KeyNotFoundException($"Concert with ID: {id}, was not found.");
            }

            return concert;
        }
    }
}

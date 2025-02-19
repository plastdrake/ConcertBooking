using ConcertBooking.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.Data.Repository
{
    public interface IConcertRepository : IRepository<Concert>
    {
        Task<IEnumerable<Concert>> GetAllConcertsAsync();
        Task<Concert> GetConcertByIdAsync(int id);
    }
}

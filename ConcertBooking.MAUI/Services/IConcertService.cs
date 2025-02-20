using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface IConcertService
    {
        Task<ObservableCollection<Concert>?> GetConcertsAsync();
        Task<Concert>? GetConcertByIdAsync(int concertId);
    }
}

using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public partial class ConcertService : IConcertService
    {
        IRestService _restService;

        public ConcertService(IRestService service)
        {
            _restService = service;
        }

        public Task<ObservableCollection<Concert>?> GetConcertsAsync()
        {
            return _restService.RefreshConcertDataAsync();
        }

        public Task<Concert?> GetConcertByIdAsync(int id)
        {
            return _restService.GetConcertByIdAsync(id);
        }
    }
}

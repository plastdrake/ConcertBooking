using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBooking.MAUI.Models;
using ConcertBooking.MAUI.Services;
using ConcertBooking.DTO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.ViewModels
{
    public partial class BookingViewModel : ObservableObject
    {
        private readonly IRestService _restService;

        [ObservableProperty]
        private ObservableCollection<Concert> concertItems;

        [ObservableProperty]
        private ObservableCollection<Performance> performances;

        [ObservableProperty]
        private ObservableCollection<Booking> bookings;

        [ObservableProperty]
        private bool isConcertsView = true;

        [ObservableProperty]
        private bool isPerformancesView = false;

        [ObservableProperty]
        private bool isBookingsView = false;


        public BookingViewModel(IRestService restService)
        {
            _restService = restService;
            concertItems = new ObservableCollection<Concert>();
            performances = new ObservableCollection<Performance>();
            bookings = new ObservableCollection<Booking>();
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            await LoadConcertsAsync();
        }

        [RelayCommand]
        public async Task LoadConcertsAsync()
        {
            var concerts = await _restService.RefreshConcertDataAsync();
            if (concerts != null)
            {
                ConcertItems.Clear();
                foreach (var concert in concerts)
                {
                    ConcertItems.Add(concert);
                }
            }
            SetViewState(concertsView: true);
        }

        [RelayCommand]
        public async Task ShowPerformancesAsync(Concert concert)
        {
            if (concert == null) return;

            try
            {
                var fetchedPerformances = await _restService.GetPerformancesAsync(concert.Id, GetCustomerId());
                if (fetchedPerformances != null)
                {
                    performances.Clear();
                    foreach (var performance in fetchedPerformances)
                    {
                        performances.Add(performance);
                    }
                }
                SetViewState(performancesView: true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading performances: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load performances.", "OK");
            }
        }


        [RelayCommand]
        public async Task BookPerformanceAsync(Performance performance)
        {
            if (performance == null) return;

            try
            {
                var bookingDto = new BookingCreateDTO { CustomerId = GetCustomerId(), PerformanceId = performance.Id };
                var success = await _restService.CreateBookingAsync(bookingDto);
                if (success)
                {
                    await LoadBookingsAsync();
                    await ShowPerformancesAsync(ConcertItems.FirstOrDefault(c => c.Id == performance.ConcertId));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to create booking.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error booking performance: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to book performance.", "OK");
            }
        }

        [RelayCommand]
        public async Task LoadBookingsAsync()
        {
            try
            {
                var fetchedBookings = await _restService.GetBookingsByCustomerIdAsync(GetCustomerId());
                if (fetchedBookings != null)
                {
                    bookings.Clear();
                    foreach (var booking in fetchedBookings)
                    {
                        bookings.Add(booking);
                    }
                }
                SetViewState(bookingsView: true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading bookings: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load bookings.", "OK");
            }
        }

        private void SetViewState(bool concertsView = false, bool performancesView = false, bool bookingsView = false)
        {
            isConcertsView = concertsView;
            isPerformancesView = performancesView;
            isBookingsView = bookingsView;
        }

        private int GetCustomerId()
        {
            return Preferences.Get("CustomerId", 0);
        }
    }
}
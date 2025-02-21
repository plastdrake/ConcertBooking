using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBooking.MAUI.Models;
using ConcertBooking.MAUI.Services;
using ConcertBooking.DTO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        private bool isBookingsView = false;

        [ObservableProperty]
        private string availableConcertsTitle = "Available Concerts";

        [ObservableProperty]
        private string bookedConcertsTitle = "Your Booked Concerts";

        public BookingViewModel(IRestService restService)
        {
            Debug.WriteLine("BookingViewModel constructor called");
            _restService = restService;
            concertItems = new ObservableCollection<Concert>();
            performances = new ObservableCollection<Performance>();
            bookings = new ObservableCollection<Booking>();
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            await LoadConcertsAsync();
            await LoadBookingsAsync();
            SetViewState(concertsView: true, bookingsView: true);
        }

        [RelayCommand]
        public async Task LoadConcertsAsync()
        {
            Debug.WriteLine("Starting LoadConcertsAsync");
            var concerts = await _restService.RefreshConcertDataAsync();
            Debug.WriteLine($"Received concerts: {concerts?.Count ?? 0}");

            if (concerts != null)
            {
                ConcertItems.Clear();
                foreach (var concert in concerts)
                {
                    ConcertItems.Add(concert);
                    Debug.WriteLine($"Added concert: {concert.Title}");
                }
                Debug.WriteLine($"Loaded concerts: {ConcertItems.Count} concerts loaded.");
            }
            else
            {
                Debug.WriteLine("Concerts were null");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load concerts.", "OK");
            }
        }

        [RelayCommand]
        public async Task ShowPerformancesAsync(Concert concert)
        {
            if (concert == null) return;

            Debug.WriteLine($"Loading performances for concert {concert.Id}");
            var fetchedPerformances = await _restService.GetPerformancesAsync(concert.Id, GetCustomerId());
            if (fetchedPerformances != null)
            {
                concert.Performances.Clear();
                foreach (var performance in fetchedPerformances)
                {
                    concert.Performances.Add(performance);
                }
            }
        }

        [RelayCommand]
        public async Task BookPerformanceAsync(Performance performance)
        {
            Debug.WriteLine("BookPerformanceAsync called");
            if (performance == null)
            {
                Debug.WriteLine("Performance is null");
                return;
            }

            Debug.WriteLine($"Booking performance {performance.Id}");
            var bookingDto = new BookingCreateDTO { CustomerId = GetCustomerId(), PerformanceId = performance.Id };
            var success = await _restService.CreateBookingAsync(bookingDto);
            if (success)
            {
                Debug.WriteLine("Booking successful");
                await LoadBookingsAsync();
                await ShowPerformancesAsync(ConcertItems.FirstOrDefault(c => c.Id == performance.ConcertId));
            }
            else
            {
                Debug.WriteLine("Booking failed");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to create booking.", "OK");
            }
        }

        [RelayCommand]
        public async Task LoadBookingsAsync()
        {
            var fetchedBookings = await _restService.GetBookingsByCustomerIdAsync(GetCustomerId());
            if (fetchedBookings != null)
            {
                Bookings.Clear();
                foreach (var booking in fetchedBookings)
                {
                    Bookings.Add(booking);
                }
            }
        }

        [RelayCommand]
        public async Task CancelBookingAsync(Booking booking)
        {
            Debug.WriteLine("CancelBookingAsync called");
            if (booking == null)
            {
                Debug.WriteLine("Booking is null");
                return;
            }

            Debug.WriteLine($"Cancelling booking {booking.BookingId}");
            var success = await _restService.DeleteBookingAsync(booking.BookingId);
            if (success)
            {
                Debug.WriteLine("Cancellation successful");
                await LoadBookingsAsync();
            }
            else
            {
                Debug.WriteLine("Cancellation failed");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to cancel booking.", "OK");
            }
        }

        [RelayCommand]
        public void Logout()
        {
            Preferences.Set("IsLoggedIn", false);
            Preferences.Set("CustomerId", 0);
            Shell.Current.GoToAsync("//LoginPage");
        }

        private void SetViewState(bool concertsView = false, bool bookingsView = false)
        {
            IsConcertsView = concertsView;
            IsBookingsView = bookingsView;
        }

        private int GetCustomerId()
        {
            return Preferences.Get("CustomerId", 0);
        }
    }
}
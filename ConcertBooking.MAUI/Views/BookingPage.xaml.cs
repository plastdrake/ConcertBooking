using ConcertBooking.MAUI.Models;
using ConcertBooking.MAUI.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;

namespace ConcertBooking.MAUI.Views
{
    public partial class BookingPage : ContentPage
    {
        private readonly BookingViewModel _viewModel;

        public BookingPage(BookingViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            this.Loaded += BookingPage_Loaded;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _viewModel;
        }

        private async void BookingPage_Loaded(object sender, EventArgs e)
        {
            if (!Preferences.Get("IsLoggedIn", false))
            {
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }
            await _viewModel.LoadDataAsync();
        }

        private async void OnBookButtonClicked(object sender, EventArgs e)
        {
            var performance = (sender as Button)?.CommandParameter as Performance;
            if (performance != null)
            {
                Debug.WriteLine($"Book Button Clicked: {performance.Id} - {performance.Venue}");
                await _viewModel.BookPerformanceAsync(performance);
            }
            else
            {
                Debug.WriteLine("Performance is null in OnBookButtonClicked");
            }
        }

        private async void OnCancelBookingButtonClicked(object sender, EventArgs e)
        {
            var booking = (sender as Button)?.CommandParameter as Booking;
            if (booking != null)
            {
                Debug.WriteLine($"Cancel Booking Button Clicked: {booking.BookingId} - {booking.ConcertTitle}");
                await _viewModel.CancelBookingAsync(booking);
            }
            else
            {
                Debug.WriteLine("Booking is null in OnCancelBookingButtonClicked");
            }
        }
    }
}

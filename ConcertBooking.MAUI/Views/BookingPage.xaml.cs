using ConcertBooking.MAUI.ViewModels;
using Microsoft.Maui.Controls;

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

        private async void BookingPage_Loaded(object sender, EventArgs e)
        {
            if (!Preferences.Get("IsLoggedIn", false))
            {
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }
            await _viewModel.LoadDataAsync();
        }
    }
}
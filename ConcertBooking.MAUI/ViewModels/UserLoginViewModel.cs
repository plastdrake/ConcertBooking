using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBooking.MAUI.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace ConcertBooking.MAUI.ViewModels
{
    public partial class UserLoginViewModel : ObservableObject
    {
        private readonly ICustomerService _customerService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        public UserLoginViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [RelayCommand]
        public async Task Login()
        {
            Console.WriteLine("Login command executed.");
            var customer = await _customerService.CustomerLoginAsync(Email, Password);
            if (customer != null)
            {
                Preferences.Clear();

                Preferences.Set("CustomerId", customer.Id);
                Preferences.Set("IsLoggedIn", true);

                Debug.WriteLine($"CustomerId saved in Preferences: {customer.Id}");

                await Shell.Current.GoToAsync("//BookingPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        [RelayCommand]
        public async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync("//UserRegistrationPage");
        }
    }
}

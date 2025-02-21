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

        // Properties that automatically implement INotifyPropertyChanged
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        // Constructor for injecting the service
        public UserLoginViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // LoginCommand implemented using RelayCommand
        [RelayCommand]
        public async Task Login()
        {
            var customer = await _customerService.CustomerLoginAsync(Email, Password);
            if (customer != null)
            {
                // Clear preferences before saving new customer ID
                Preferences.Clear();

                // Save the customer ID in preferences
                Preferences.Set("CustomerId", customer.Id);
                Preferences.Set("IsLoggedIn", true);

                Debug.WriteLine($"CustomerId saved in Preferences: {customer.Id}");

                // Navigate to ConcertsPage
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        // NavigateToRegisterCommand implemented using RelayCommand
        [RelayCommand]
        public async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync("RegisterPage");
        }
    }
}

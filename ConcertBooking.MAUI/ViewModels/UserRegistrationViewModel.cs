using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBooking.MAUI.Services;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.ViewModels
{
    public partial class UserRegistrationViewModel : ObservableObject
    {
        private readonly ICustomerService _customerService;

        [ObservableProperty] private string firstName;
        [ObservableProperty] private string lastName;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string passwordConfirmation;

        public UserRegistrationViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [RelayCommand]
        private async Task RegisterUserAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(PasswordConfirmation))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");
                    return;
                }

                if (Password != PasswordConfirmation)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }

                var newCustomer = new Models.Customer
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password
                };

                Console.WriteLine($"📌 Sending data to API: {System.Text.Json.JsonSerializer.Serialize(newCustomer)}");

                bool success = await _customerService.RegisterCustomerAccountAsync(newCustomer);

                if (success)
                {
                    Console.WriteLine("✅ Registration successful!");
                    await Application.Current.MainPage.DisplayAlert("Success", "Registration successful!", "OK");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    Console.WriteLine("❌ Registration failed!");
                    await Application.Current.MainPage.DisplayAlert("Error", "Registration failed. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚨 Exception during registration: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

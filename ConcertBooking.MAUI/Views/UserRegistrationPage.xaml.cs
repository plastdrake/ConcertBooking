using ConcertBooking.MAUI.ViewModels;

namespace ConcertBooking.MAUI.Views;

public partial class UserRegistrationPage : ContentPage
{
    public UserRegistrationPage(UserRegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

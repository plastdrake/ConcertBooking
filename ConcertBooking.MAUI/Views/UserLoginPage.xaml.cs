using ConcertBooking.MAUI.ViewModels;

namespace ConcertBooking.MAUI.Views;

public partial class UserLoginPage : ContentPage
{
    public UserLoginPage(UserLoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

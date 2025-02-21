using Microsoft.Maui.Controls;

namespace ConcertBooking.MAUI.Views
{
    public class BindingProxy : BindableObject
    {
        public object Binding
        {
            get => GetValue(BindingProperty);
            set => SetValue(BindingProperty, value);
        }
        public static readonly BindableProperty BindingProperty =
            BindableProperty.Create(nameof(Binding), typeof(object), typeof(BindingProxy), null);
    }
}

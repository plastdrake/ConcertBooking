using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ConcertBooking.MAUI.Services;
using ConcertBooking.MAUI.ViewModels;
using ConcertBooking.MAUI.Views;

namespace ConcertBooking.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<IRestService, RestService>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<BookingPage>();

            return builder.Build();
        }
    }
}
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ConcertBooking.MAUI.Services;
using ConcertBooking.MAUI.ViewModels;
using ConcertBooking.MAUI.Views;
using ConcertBooking.MAUI.Profiles;
using AutoMapper;

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
            // Register Pages
            builder.Services.AddTransient<BookingPage>();
            builder.Services.AddTransient<UserLoginPage>();
            builder.Services.AddTransient<UserRegistrationPage>();

            // Register Shared Services
            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<IBookingService, BookingService>();
            builder.Services.AddSingleton<IConcertService, ConcertService>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();

            // Register AutoMapper
            builder.Services.AddSingleton<IMapper>(new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(BookingProfile).Assembly); // Automatically loads all profiles
            }).CreateMapper());

            // Register ViewModels
            builder.Services.AddTransient<UserLoginViewModel>();
            builder.Services.AddSingleton<BookingViewModel>();
            builder.Services.AddTransient<UserRegistrationViewModel>();

            return builder.Build();
        }
    }
}

using AutoMapper;
using ConcertBooking.MAUI.Models;
using ConcertBooking.DTO;

namespace ConcertBooking.MAUI.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, Booking>()
             .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
             .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
             .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.CustomerLastName))
             .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
             .ForMember(dest => dest.PerformanceDate, opt => opt.MapFrom(src => src.PerformanceDate))
             .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
             .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
             .ForMember(dest => dest.ConcertTitle, opt => opt.MapFrom(src => src.ConcertTitle));

            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
                .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.CustomerLastName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.PerformanceDate, opt => opt.MapFrom(src => src.PerformanceDate))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ConcertTitle, opt => opt.MapFrom(src => src.ConcertTitle));

            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.PerformanceId, opt => opt.MapFrom(src => src.PerformanceId));

            CreateMap<Booking, BookingCreateDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.PerformanceId, opt => opt.MapFrom(src => src.PerformanceId));
        }
    }
}

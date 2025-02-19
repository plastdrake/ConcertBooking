using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;

namespace ConcertBooking.MAUI.Profiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            CreateMap<Performance, PerformanceDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PerformanceDate, opt => opt.MapFrom(src => src.PerformanceDateAndTime))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForPath(dest => dest.ConcertTitle, opt => opt.MapFrom(src => src.Concert.Title));

            CreateMap<PerformanceDTO, Performance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.PerformanceDateAndTime, opt => opt.MapFrom(src => src.PerformanceDate))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}

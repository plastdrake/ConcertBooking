using AutoMapper;
using ConcertBooking.Data.Entity;
using ConcertBooking.DTO;
using ConcertBooking.Data.DTO;
using ConcertBooking.Data.Entity;

namespace ConcertBooking.API.Profiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            // Map Entity to DTO
            // Note that ConcertTitle does not exists in the Performance entity, it is a derived property from the Concert entity.

            CreateMap<Performance, PerformanceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PerformanceDate, opt => opt.MapFrom(src => src.PerformanceDateAndTime))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForPath(dest => dest.ConcertTitle, opt => opt.MapFrom(src => src.Concert.Title));

            // Map DTO to Entity
            // Note that ConcertTitle does not exists in the Performance entity, it is a derived property from the Concert entity.

            CreateMap<PerformanceDTO, Performance>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PerformanceDateAndTime, opt => opt.MapFrom(src => src.PerformanceDate))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForPath(dest => dest.Concert.Title, opt => opt.MapFrom(src => src.ConcertTitle));
        }
    }
}

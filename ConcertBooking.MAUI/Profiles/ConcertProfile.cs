using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;

namespace ConcertBooking.MAUI.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<Concert, ConcertDTO>()
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Performances, opt => opt.MapFrom(src => src.Performances));

            CreateMap<ConcertDTO, Concert>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ConcertId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Performances, opt => opt.MapFrom(src => src.Performances));
        }
    }
}

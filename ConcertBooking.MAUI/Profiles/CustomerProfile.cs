using AutoMapper;
using ConcertBooking.Data.DTO;
using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;

namespace ConcertBooking.MAUI.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerFirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.CustomerLastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.PasswordConfirmation, opt => opt.Ignore());

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Customer, CustomerRegisterDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Customer, CustomerUpdateDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Customer, CustomerRegisterDTO>().ReverseMap();
            CreateMap<Customer, LoginDTO>().ReverseMap();
        }
    }
}

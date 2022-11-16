using AutoMapper;
using BL.DTOs.Reservation;
using BL.DTOs.User;
using DAL.Entities;

namespace BL
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Reservation, ReservationsDto>()
                .ForMember(dest => dest.BookTitle, act => act.MapFrom(src => src.BookPrint.Book.Title)).ReverseMap();
            config.CreateMap<User, UserDetailDto>().ReverseMap();
        }
    }
}

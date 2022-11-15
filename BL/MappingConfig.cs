using AutoMapper;
using BL.DTOs;
using DAL.Entities;

namespace BL
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Reservation, ReservationsDto>()
                .ForMember(dest => dest.BookTitle, act => act.MapFrom(src => src.BookPrint.Book.Title));
        }
    }
}

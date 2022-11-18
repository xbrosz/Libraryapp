using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Branch;
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
            config.CreateMap<Rating, RatingDto>().ReverseMap();
            config.CreateMap<Author, AuthorDto>().ReverseMap();
            config.CreateMap<Book, BookDetailDto>().ReverseMap();
            config.CreateMap<Book, BookGridDto>().ReverseMap();
            config.CreateMap<BookPrint, BookPrintDto>().ReverseMap();
            config.CreateMap<Branch, BranchDto>().ReverseMap();

            config.CreateMap<QueryResultDto<Author>, Author>();
            config.CreateMap<QueryResultDto<Branch>, Branch>();
        }
    }
}

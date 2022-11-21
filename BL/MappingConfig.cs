using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.DTOs.User;
using DAL.Entities;
using Infrastructure.Query;

namespace BL
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Reservation, ReservationsDto>()
                .ForMember(dest => dest.BookTitle, act => act.MapFrom(src => src.BookPrint.Book.Title)).ReverseMap();
            config.CreateMap<User, UserDetailDto>().ReverseMap();
            config.CreateMap<User, CreateUserDto>().ReverseMap();
            config.CreateMap<Rating, RatingDto>().ReverseMap();
            config.CreateMap<Author, AuthorDto>().ReverseMap();
            config.CreateMap<Book, BookDetailDto>().ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName + " "
                                                                                                               + src.Author.MiddleName + " "
                                                                                                               + src.Author.LastName))
                                                   .ForMember(dest => dest.BookGenres, act => act.MapFrom(src => string.Join("/", src.Genres.Select(g => g.Name))));
            config.CreateMap<Book, BookGridDto>().ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName + " "
                                                                                                             + src.Author.MiddleName + " "
                                                                                                             + src.Author.LastName))
                                                 .ForMember(dest => dest.BookGenres, act => act.MapFrom(src => string.Join("/", src.Genres.Select(g => g.Name))))
                                                 .ForMember(dest => dest.Rating, act => act.MapFrom(src => src.Ratings.Select(r => r.RatingNumber).Average()));
            config.CreateMap<BookPrint, BookPrintDto>().ReverseMap();
            config.CreateMap<Branch, BranchDto>().ReverseMap();

            config.CreateMap<QueryResultDto<AuthorDto>, EFQueryResult<Author>>().ReverseMap();
            config.CreateMap<QueryResultDto<BranchDto>, EFQueryResult<Branch>>().ReverseMap();
            config.CreateMap<QueryResultDto<BookPrintDto>, EFQueryResult<BookPrint>>().ReverseMap();
            config.CreateMap<QueryResultDto<BookGridDto>, EFQueryResult<Book>>().ReverseMap();

        }
    }
}

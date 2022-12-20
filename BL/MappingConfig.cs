using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.BookGenre;
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
            config.CreateMap<Branch, BranchDto>().ReverseMap();

            config.CreateMap<Reservation, ReservationsDto>()
                .ForMember(dest => dest.BookTitle, act => act.MapFrom(src => src.BookPrint.Book.Title))
                .ForMember(dest => dest.Branch, act => act.MapFrom(src => src.BookPrint.Branch))
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.BookPrint.Book.Id));
            config.CreateMap<Reservation, UpdateReservationDto>().ReverseMap();

            config.CreateMap<User, UserDetailDto>().ForMember(dest => dest.RoleName, act => act.MapFrom(src => src.Role.Name));

            config.CreateMap<User, UserCreateDto>().ReverseMap();
            config.CreateMap<User, UserUpdateDto>().ReverseMap();
            config.CreateMap<Rating, RatingDto>()
                .ForMember(dest => dest.BookTitle, act => act.MapFrom(src => src.Book.Title));
            config.CreateMap<RatingDto, Rating>();
            config.CreateMap<Author, AuthorDto>().ReverseMap();

            config.CreateMap<Book, BookDetailDto>().ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName + " "
                                                                                                               + src.Author.MiddleName + " "
                                                                                                               + src.Author.LastName));
                                                 //.ForMember(dest => dest.BookGenres, act => act.MapFrom(src => string.Join("/", src.Genres.Select(g => g.Name))));

            config.CreateMap<Book, BookGridDto>().ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName + " "
                                                                                                             + src.Author.MiddleName + " "
                                                                                                             + src.Author.LastName));
                                                 //.ForMember(dest => dest.BookGenres, act => act.MapFrom(src => string.Join("/", src.Genres.Select(g => g.Name))))
                                                 //.ForMember(dest => dest.Rating, act => act.MapFrom(src => src.Ratings.Select(r => r.RatingNumber).Average()));
            
            config.CreateMap<BookPrint, BookPrintDto>().ReverseMap();

            config.CreateMap<BookGenre, BookGenreDto>().ReverseMap();

            config.CreateMap<QueryResultDto<AuthorDto>, EFQueryResult<Author>>().ReverseMap();
            config.CreateMap<QueryResultDto<BranchDto>, EFQueryResult<Branch>>().ReverseMap();
            config.CreateMap<QueryResultDto<RatingDto>, EFQueryResult<Rating>>().ReverseMap();
            config.CreateMap<QueryResultDto<BookGridDto>, EFQueryResult<Book>>().ReverseMap();
            config.CreateMap<QueryResultDto<BookPrintDto>, EFQueryResult<BookPrint>>().ReverseMap();
            config.CreateMap<QueryResultDto<RatingDto>, EFQueryResult<Rating>>().ReverseMap();
            config.CreateMap<QueryResultDto<ReservationsDto>, EFQueryResult<Reservation>>().ReverseMap();
            config.CreateMap<QueryResultDto<UserDetailDto>, EFQueryResult<User>>().ReverseMap();
            config.CreateMap<QueryResultDto<BookGenreDto>, EFQueryResult<BookGenre>>().ReverseMap();

            config.CreateMap<CreateReservationDto, Reservation>().ReverseMap();
        }
    }
}

using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.DTOs.Genre;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Tests.Services
{
    public class BookServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<BookFilterDto, BookGridDto>> _queryObjectMock;
        Mock<IRepository<Book>> _repoMock;
        Mock<IQueryObject<BookGenreFilterDto, BookGenreDto>> _bookGenreQueryObject;
        Mock<IQueryObject<GenreDto, GenreDto>> _genreQueryObject;

        public BookServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<BookFilterDto, BookGridDto>>();
            _repoMock = new Mock<IRepository<Book>>();
            _bookGenreQueryObject = new Mock<IQueryObject<BookGenreFilterDto, BookGenreDto>>();
            _genreQueryObject = new Mock<IQueryObject<GenreDto, GenreDto>>();
        }

        [Fact]
        public void GetBookDetailByIDTest()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var book = new Book

            {
                Id = 1,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "J.",
                    MiddleName = "R.R.",
                    LastName = "Tolkien",
                    BirthDate = new DateTime(1900, 6, 6)
                },
                Genres = new List<Genre> { new Genre { Id = 1, Name = "Anime" }, new Genre { Id = 2, Name = "Horror" } },
                Ratings = new List<Rating> { },
                Release = new DateTime(2000, 6, 6),
                Title = "C++ for begginers"
            };
            var bookPrintDto = new BookPrintDto
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
            };

            var query = new QueryResultDto<BookPrintDto>
            {
                Items = new List<BookPrintDto>() { bookPrintDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(book);

            _uowMock
                .Setup(x => x.BookRepository)
                .Returns(_repoMock.Object);

            var service = new BookService(_uowMock.Object, mapper, _queryObjectMock.Object, _bookGenreQueryObject.Object, _genreQueryObject.Object);
            var resultDto = service.GetBookDetailByID(1);
            Assert.True(resultDto.Release == new DateTime(2000, 6, 6) &&
                        resultDto.Id == 1 &&
                        resultDto.Title == "C++ for begginers" &&
                        resultDto.AuthorName == "J. R.R. Tolkien" &&
                        resultDto.BookGenres == "Anime/Horror");
        }

    }
}

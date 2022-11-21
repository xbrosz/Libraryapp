using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using Infrastructure.UnitOfWork;

namespace BL.Tests.Services
{
    public class AuthorServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<AuthorFilterDto, AuthorDto>> _queryObjectMock;
        private Mock<IMapper> _mapperMock;

        public AuthorServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<AuthorFilterDto, AuthorDto>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void GetAuthorsByName_NameWithLetters()
        {
            var authorDto = new AuthorDto()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Petrovsky",
                LastName = "Petrovitansky",
                BirthDate = DateTime.Now
            };

            var queryResult = new QueryResultDto<AuthorDto>
            {
                Items = new List<AuthorDto>() { authorDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<AuthorFilterDto>()))
                .Returns(queryResult);

            var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

            var expectedOutput = new List<AuthorDto>() { authorDto };

            var realOutput = service.GetAuthorsByName("Peter", "Petrovsky", "Petrovitansky");

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<AuthorFilterDto>()), Times.Once);
            Assert.Equal(expectedOutput, realOutput);
        }

        [Fact]
        public void GetAuthorsByName_NameWithDigits()
        {
            var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

            var exception = Assert.Throws<Exception>(() => service.GetAuthorsByName("P6t6r", "Petrovsky", "Petrovitansky"));

            Assert.Equal("Names should contain just letters.", exception.Message);
        }

        [Fact]
        public void GetAuthorsByName_NameWithSpecCharacters()
        {
            var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

            var exception = Assert.Throws<Exception>(() => service.GetAuthorsByName("Peter-", "Petrovsky", "Petrovitansky"));

            Assert.Equal("Names should contain just letters.", exception.Message);
        }
    }
}

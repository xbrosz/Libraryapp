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
        Mock<IQueryObject<AuthorFilterDto, AuthorGridDto>> _queryObjectMock;
        private Mock<IMapper> _mapperMock;

        public AuthorServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<AuthorFilterDto, AuthorGridDto>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void GetAuthorsByName_NameWithLetters()
        {
            var authorDto = new AuthorGridDto()
            {
                Id = 1,
                Name = "Peter Petrovsky Petrovitansky",
                BirthDate = DateTime.Now
            };

            var queryResult = new QueryResultDto<AuthorGridDto>
            {
                Items = new List<AuthorGridDto>() { authorDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<AuthorFilterDto>()))
                .Returns(queryResult);

            var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

            var expectedOutput = new List<AuthorGridDto>() { authorDto };

            var realOutput = service.GetAuthorsByName(new AuthorFilterDto() { FirstName=  "Peter" });

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<AuthorFilterDto>()), Times.Once);
            Assert.Equal(expectedOutput, realOutput);
        }

        //[Fact]
        //public void GetAuthorsByName_NameWithDigits()
        //{
        //    var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

        //    var exception = Assert.Throws<Exception>(() => service.GetAuthorsByName(new AuthorFilterDto() { FirstName = "P6t6r" }));

        //    Assert.Equal("Names should contain just letters.", exception.Message);
        //}

        //[Fact]
        //public void GetAuthorsByName_NameWithSpecCharacters()
        //{
        //    var service = new AuthorService(_uowMock.Object, _mapperMock.Object, _queryObjectMock.Object);

        //    var exception = Assert.Throws<Exception>(() => service.GetAuthorsByName(new AuthorFilterDto() { FirstName = "Peter-" }));

        //    Assert.Equal("Names should contain just letters.", exception.Message);
        //}
    }
}

using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.QueryObjects.QueryObjects;
using DAL.Entities;
using Infrastructure.Query;
using System.Linq.Expressions;

namespace BL.Tests.QueryObjects
{
    public class AuthorQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IAbstractQuery<Author>> _queryMock;

        public AuthorQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IAbstractQuery<Author>>();
        }

        [Fact]
        public void FilterAuthors()
        {
            var author = new Author()
            {
                Id = 1,
                FirstName = "Peter",
                MiddleName = "Petrovsky",
                LastName = "Petrovitansky",
                BirthDate = DateTime.Now
            };

            var authorDto = new AuthorGridDto()
            {
                Id = 1,
                Name = "Peter Petrovsky Petrovitansky",
                BirthDate = DateTime.Now
            };

            var queryResultDto = new QueryResultDto<AuthorGridDto>()
            {
                Items = new List<AuthorGridDto>() { authorDto },
                TotalItemsCount = 1
            };

            var efQueryResult = new EFQueryResult<Author>()
            {
                Items = new List<Author>() { author },
                TotalItemsCount = 1
            };

            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.OrderBy<string>(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Execute())
                .Returns(efQueryResult);

            _mapperMock
                .Setup(x => x.Map<QueryResultDto<AuthorGridDto>>(It.IsAny<EFQueryResult<Author>>()))
                .Returns(queryResultDto);

            var queryObject = new AuthorQueryObject(_mapperMock.Object, _queryMock.Object);

            var filterDto = new AuthorFilterDto()
            {
                FirstName = "Peter",
                MiddleName = "Petrovsky",
                LastName = "Petrovitansky",
                PageSize = 5,
                RequestedPageNumber = 1,
                SortCriteria = "FirstName",
                SortAscending = true
            };

            var res = queryObject.ExecuteQuery(filterDto);

            Assert.Equal(queryResultDto, res);
            _queryMock.Verify(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()), Times.Exactly(3));
            _queryMock.Verify(x => x.Page(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _queryMock.Verify(x => x.OrderBy<string>(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _queryMock.Verify(x => x.Execute(), Times.Once);
        }
    }
}

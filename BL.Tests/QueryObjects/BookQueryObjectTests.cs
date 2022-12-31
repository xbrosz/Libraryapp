using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.QueryObjects;
using DAL.Entities;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.QueryObjects
{
    public class BookQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IAbstractQuery<Book>> _queryMock;

        public BookQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IAbstractQuery<Book>>();
        }

        //[Fact]
        //public void FilterBook()
        //{

        //    var book = new Book
        //    {
        //        Author = new Author
        //        {
        //            BirthDate = DateTime.Now,
        //            FirstName = "A",
        //            LastName = "B",
        //            Id = 1
        //        },
        //        Genres = new List<Genre>(),
        //        Id = 1,
        //        Ratings = new List<Rating>(),
        //        Release = new DateTime(1900,1,1),
        //        Title = "Book"
        //    };
            
        //    var queryResultDto = new QueryResultDto<BookGridDto>
        //    {
        //        Items = new List<BookGridDto>(),
        //        TotalItemsCount = 0
        //    };
        //    var efQueryResult = new EFQueryResult<Book>() { Items = new List<Book>(), TotalItemsCount = 0 };

        //    _queryMock
        //        .Setup(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()))
        //        .Returns(_queryMock.Object);

        //    _queryMock
        //        .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
        //        .Returns(_queryMock.Object);

        //    _queryMock
        //        .Setup(x => x.Execute())
        //        .Returns(efQueryResult);

        //    _mapperMock
        //        .Setup(x => x.Map<QueryResultDto<BookGridDto>>(It.IsAny<EFQueryResult<Book>>()))
        //        .Returns(queryResultDto);

        //    var queryObject = new BookQueryObject(_mapperMock.Object, _queryMock.Object);
        //    var filter = new BookFilterDto
        //    {
        //        AuthorID = 2
        //    };
        //    var result = queryObject.ExecuteQuery(filter);
        //    Assert.Empty(result.Items);
        //}
    }
}

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
    public class BookPrintQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IAbstractQuery<BookPrint>> _queryMock;

        public BookPrintQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IAbstractQuery<BookPrint>>();
        }
        [Fact]
        public void FilterBookPrints()
        {
            BookPrint bookPrint = new BookPrint
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
                
            };
            BookPrintDto dto = new BookPrintDto
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
            };
            var queryResultDto = new QueryResultDto<BookPrintDto>
            {
                Items = new List<BookPrintDto> { dto },
                TotalItemsCount = 1
            };
            var efQueryResult = new EFQueryResult<BookPrint>() { Items = new List<BookPrint>() { bookPrint }, TotalItemsCount = 1 };
            
            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Execute())
                .Returns(efQueryResult);

            _mapperMock
                .Setup(x => x.Map<QueryResultDto<BookPrintDto>>(It.IsAny<EFQueryResult<BookPrint>>()))
                .Returns(queryResultDto);

            var queryObject = new BookPrintQueryObject(_mapperMock.Object, _queryMock.Object);
            var filter = new BookPrintFilterDto
            {
                BookId = 1,
                BranchId = 1
            };
            var result = queryObject.ExecuteQuery(filter);
            Assert.True(result is null);
        }
    }
}

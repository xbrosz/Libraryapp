using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.QueryObjects.QueryObjects;
using DAL.Data;
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
    public class ReservationQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IReservationQuery> _queryMock;

        public ReservationQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IReservationQuery>();
        }

        [Fact]
        public void FilterReservations()
        {
            var reservation = new Reservation() { Id = 1, BookPrintId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now };

            var reservationDto1 = new ReservationsDto() { Id = 1, BookPrintId = 1, BookTitle = "title1" };
            var reservationDto2 = new ReservationsDto() { Id = 2, BookPrintId = 2, BookTitle = "title2" };

            var queryResultDto = new QueryResultDto<ReservationsDto>()
            {
                Items = new List<ReservationsDto>() { reservationDto1, reservationDto2 },
                TotalItemsCount = 2
            };

            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<int, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<BookPrint, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.ToFilter(It.IsAny<DateTime>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.FromFilter(It.IsAny<DateTime>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Execute())
                .Returns(new List<Reservation>() { reservation });

            _mapperMock
                .Setup(x => x.Map<QueryResultDto<ReservationsDto>>(It.IsAny<IEnumerable<Reservation>>()))
                .Returns(queryResultDto);

            var queryObject = new ReservationQueryObject(_mapperMock.Object, _queryMock.Object);

            var filterDto = new ReservationFilterDto()
            {
                BookId = 1,
                BranchId = 1,
                UserId = 1,
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
                PageSize = 5,
                RequestedPageNumber = 1
            };

            var res = queryObject.ExecuteQuery(filterDto);

            Assert.Equal(queryResultDto, res);
            _queryMock.Verify(x => x.Where(It.IsAny<Expression<Func<int, bool>>>(), It.IsAny<string>()), Times.Once);
            _queryMock.Verify(x => x.Where(
                It.IsAny<Expression<Func<BookPrint, bool>>>(), It.IsAny<string>()
                ), Times.Exactly(2));
            _queryMock.Verify(x => x.ToFilter(filterDto.ToDate.Value), Times.Once);
            _queryMock.Verify(x => x.FromFilter(filterDto.FromDate.Value), Times.Once);
            _queryMock.Verify(x => x.Page(filterDto.RequestedPageNumber.Value, filterDto.PageSize), Times.Once);
            _queryMock.Verify(x => x.Execute(), Times.Once);
        }

    }
}

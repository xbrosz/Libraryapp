﻿using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Tests.Services
{
    public class ReservationServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<ReservationFilterDto, ReservationsDto>> _queryObjectMock;
        Mock<IRepository<Reservation>> _repoMock;

        public ReservationServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<ReservationFilterDto, ReservationsDto>>();
            _repoMock = new Mock<IRepository<Reservation>>();
        }

        [Fact]
        public void FilterReservationsByBookId()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var resultDto = new ReservationsDto()
            {
                Id = 1,
                BookPrintId = 1,
                BookTitle = "title"
            };

            var queryResult = new QueryResultDto<ReservationsDto>()
            {
                Items = new List<ReservationsDto>() { resultDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()))
                .Returns(queryResult);

            _uowMock
                .Setup(x => x.ReservationRepository)
                .Returns(_repoMock.Object);

            var service = new ReservationService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var res = service.GetReservationsByBookId(1);

            Assert.Single(res);
            Assert.Equal(res.First(), resultDto);

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()), Times.Once);
            _uowMock.Verify(x => x.ReservationRepository, Times.Once);
        }

        [Fact]
        public void FilterReservationsByUserId()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var resultDto = new ReservationsDto()
            {
                Id = 1,
                BookPrintId = 1,
                BookTitle = "title"
            };

            var queryResult = new QueryResultDto<ReservationsDto>()
            {
                Items = new List<ReservationsDto>() { resultDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()))
                .Returns(queryResult);

            _uowMock
                .Setup(x => x.ReservationRepository)
                .Returns(_repoMock.Object);

            var service = new ReservationService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var res = service.GetReservationsByUserId(1);

            Assert.Single(res);
            Assert.Equal(res.First(), resultDto);

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()), Times.Once);
            _uowMock.Verify(x => x.ReservationRepository, Times.Once);
        }

        [Fact]
        public void FilterReservationsByDateRange()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var resultDto = new ReservationsDto()
            {
                Id = 1,
                BookPrintId = 1,
                BookTitle = "title"
            };

            var queryResult = new QueryResultDto<ReservationsDto>()
            {
                Items = new List<ReservationsDto>() { resultDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()))
                .Returns(queryResult);

            _uowMock
                .Setup(x => x.ReservationRepository)
                .Returns(_repoMock.Object);

            var service = new ReservationService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var res = service.GetReservationsInDateRangeByBookAndBranch(1, 1, DateTime.Now, DateTime.Now);

            Assert.Single(res);
            Assert.Equal(res.First(), resultDto);

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<ReservationFilterDto>()), Times.Once);
            _uowMock.Verify(x => x.ReservationRepository, Times.Once);
        }
    }
}

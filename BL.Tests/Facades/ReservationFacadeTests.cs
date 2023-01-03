using BL.DTOs;
using BL.DTOs.Reservation;
using BL.Facades;
using BL.Facades.Facades;
using BL.Services.IServices;

namespace BL.Tests.Facades
{
    public class ReservationFacadeTests
    {
        Mock<IReservationService> _reservationServiceMock;
        Mock<IBookPrintService> _bpServiceMock;
        Mock<IBranchService> _branchServiceMock;
        public ReservationFacadeTests()
        {
            _reservationServiceMock = new Mock<IReservationService>();
            _bpServiceMock = new Mock<IBookPrintService>();
            _branchServiceMock = new Mock<IBranchService>();
        }

        [Fact]
        public void ReserveBook_Successful()
        {
            var reservation = new ReservationsDto() { Id = 1, BookPrintId = 1, BookTitle = "title" };

            var reservations = new List<ReservationsDto>() { reservation };

            _reservationServiceMock
                .Setup(x => x.GetReservationsInDateRangeByBookAndBranch(
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())
                ).Returns(reservations);

            var bookPrint1 = new BookPrintDto() { Id = 1, BookId = 1, BranchId = 1 };
            var bookPrint2 = new BookPrintDto() { Id = 2, BookId = 1, BranchId = 1 };

            var bookPrints = new List<BookPrintDto>() { bookPrint1, bookPrint2 };

            _bpServiceMock
                .Setup(x => x.GetBookPrintsByBranchIDAndBookID(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(bookPrints);

            var reservationFacade = new ReservationFacade(_reservationServiceMock.Object, _bpServiceMock.Object, _branchServiceMock.Object);

            var reservationForm = new ReservationCreateFormDto()
            {
                UserId = 1,
                BookId = 1,
                BranchId = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now
            };

            reservationFacade.ReserveBook(reservationForm);

            _reservationServiceMock.Verify(x => x.GetReservationsInDateRangeByBookAndBranch(
                reservationForm.BookId,
                reservationForm.BranchId,
                reservationForm.StartDate,
                reservationForm.EndDate
                ), Times.Once
            );
            _bpServiceMock.Verify(x => x.GetBookPrintsByBranchIDAndBookID(reservationForm.BranchId, reservationForm.BookId),
                Times.Once);
            _reservationServiceMock.Verify(x => x.Insert(It.IsAny<CreateReservationDto>()), Times.Once);

        }

        [Fact]
        public void ReserveBook_NoBookPrintsAvailable()
        {
            var reservation = new ReservationsDto() { Id = 1, BookPrintId = 1, BookTitle = "title" };

            var reservations = new List<ReservationsDto>() { reservation };

            _reservationServiceMock
                .Setup(x => x.GetReservationsInDateRangeByBookAndBranch(
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())
                ).Returns(reservations);

            var bookPrint1 = new BookPrintDto() { Id = 1, BookId = 1, BranchId = 1 };

            var bookPrints = new List<BookPrintDto>() { bookPrint1 };

            _bpServiceMock
                .Setup(x => x.GetBookPrintsByBranchIDAndBookID(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(bookPrints);

            var reservationFacade = new ReservationFacade(_reservationServiceMock.Object, _bpServiceMock.Object, _branchServiceMock.Object);

            var reservationForm = new ReservationCreateFormDto()
            {
                UserId = 1,
                BookId = 1,
                BranchId = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now
            };

            Exception exception = Assert.Throws<InvalidOperationException>(() => reservationFacade.ReserveBook(reservationForm));

            _reservationServiceMock.Verify(x => x.GetReservationsInDateRangeByBookAndBranch(
                reservationForm.BookId,
                reservationForm.BranchId,
                reservationForm.StartDate,
                reservationForm.EndDate
                ), Times.Once
            );
            _bpServiceMock.Verify(x => x.GetBookPrintsByBranchIDAndBookID(reservationForm.BranchId, reservationForm.BookId),
                Times.Once);
            _reservationServiceMock.Verify(x => x.Insert(It.IsAny<CreateReservationDto>()), Times.Never);

            Assert.Equal("No book print is available in given date range.", exception.Message);
        }
    }
}

using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.Facades.IFacades;
using BL.Services.IServices;

namespace BL.Facades.Facades
{
    public class ReservationFacade : IReservationFacade
    {
        private IReservationService _reservationService;
        private IBookPrintService _bookPrintService;
        private IBranchService _branchService;

        public ReservationFacade(IReservationService reservationService, IBookPrintService bpService, IBranchService branchService)
        {
            _reservationService = reservationService;
            _bookPrintService = bpService;
            _branchService = branchService;
        }

        public void ReserveBook(ReservationCreateFormDto reservationDto)
        {
            var reservedBPs = _reservationService.GetReservationsInDateRangeByBookAndBranch
                (
                reservationDto.BookId,
                reservationDto.BranchId,
                reservationDto.StartDate,
                reservationDto.EndDate
                );

            var bookPrints = _bookPrintService.GetBookPrintsByBranchIDAndBookID(reservationDto.BranchId, reservationDto.BookId);

            var availableBPs = bookPrints.Where(bp => !reservedBPs.Any(r => r.BookPrintId == bp.Id));

            if (availableBPs.Count() == 0)
            {
                throw new InvalidOperationException("No book print is available in given date range.");
            }

            var availableBP = availableBPs.First();

            CreateReservationDto createDto = new()
            {
                BookPrintId = availableBP.Id,
                UserId = reservationDto.UserId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate
            };

            _reservationService.Insert(createDto);
        }

        public void UpdateReservationDate(ReservationUpdateFormDto reservationDto)
        {
            var reservation = _reservationService.Find(reservationDto.Id);

            if (reservation.EndDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("Ended reservation cannot be edited.");
            }


            var bookId = _bookPrintService.Find(reservationDto.BookPrintId).BookId;

            var reservedBPs = _reservationService.GetReservationsInDateRangeByBookAndBranch
                (
                bookId,
                reservationDto.BranchId,
                reservationDto.StartDate,
                reservationDto.EndDate
                ).Where(r => r.Id != reservationDto.Id);

            var bookPrints = _bookPrintService.GetBookPrintsByBranchIDAndBookID(reservationDto.BranchId, bookId);

            var availableBPs = bookPrints.Where(bp => !reservedBPs.Any(r => r.BookPrintId == bp.Id));

            if (availableBPs.Count() == 0)
            {
                throw new InvalidOperationException("No book print is available in given date range.");
            }

            var availableBP = availableBPs.First();

            UpdateReservationDto updateDto = new()
            {
                Id = reservationDto.Id,
                BookPrintId = availableBP.Id,
                UserId = reservationDto.UserId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate
            };

            _reservationService.Update(updateDto);
        }

        public void DeleteReservationsForUserId(int userId)
        {
            var userReservations = _reservationService.GetReservationsByUserId(userId);
            foreach(var reservation in userReservations)
            {
                _reservationService.Delete(reservation.Id);
            }
        }

        public void DeleteReservationsForBookId(int bookId)
        {
            var bookReservations = _reservationService.GetReservationsByBookId(bookId);
            foreach (var reservation in bookReservations)
            {
                _reservationService.Delete(reservation.Id);
            }
        }

        public IEnumerable<ReservationsDto> GetActiveReservationsByBookId(int bookId)
        {
            return _reservationService.GetReservationsByBookId(bookId).Where(x => x.EndDate >= DateTime.Now);
        }
        public IEnumerable<BranchDto> GetAllBranches()
        {
            return _branchService.GetAllBranches();
        }
        public int GetBranchIDByName(string name)
        {
            return _branchService.GetBranchesByName(name).First().Id;
        }

        public IEnumerable<ReservationsDto> GetReservationsByUserId(int userId)
        {
            return _reservationService.GetReservationsByUserId(userId);
        }
    }
}

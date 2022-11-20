using BL.DTOs.Reservation;
using BL.Services.IServices;
using BL.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IReservationService reservationService;
        //TODO: change to BPS interface
        private BookPrintService bookPrintService;

        public ReservationFacade(IReservationService reservationService, BookPrintService bpService)
        {
            this.reservationService = reservationService;
            this.bookPrintService = bpService;
        }

        public void ReserveBook(ReservationFormDto reservationDto)
        {
            var reservedBPs = reservationService.GetReservationsInDateRangeByBookAndBranch
                (
                reservationDto.BookId,
                reservationDto.BranchId,
                reservationDto.StartDate,
                reservationDto.EndDate
                );

            var bookPrints = bookPrintService.GetBookbyBranchIDAndBookID(reservationDto.BranchId, reservationDto.BookId);

            var availableBPs = bookPrints.Where(bp => !reservedBPs.Any(r => r.BookPrintId == bp.Id));

            if (availableBPs.Count() == 0)
            {
                //error
                return;
            }

            var availableBP = availableBPs.First();

            CreateReservationDto createDto = new CreateReservationDto {
                BookPrintId = availableBP.Id,
                UserId = reservationDto.UserId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate
            };

            reservationService.InsertAsync(createDto);
        }
    }
}

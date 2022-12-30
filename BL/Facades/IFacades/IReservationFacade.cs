using BL.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IReservationFacade
    {
        public void ReserveBook(ReservationCreateFormDto reservationDto);
        public void UpdateReservationDate(ReservationUpdateFormDto reservationDto);
        IEnumerable<ReservationsDto> GetActiveReservationsByBookId(int bookId);
        void DeleteReservationsForUserId(int userId);

        void DeleteReservationsForBookId(int bookId);

    }
}

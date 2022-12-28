using BL.DTOs.Reservation;

namespace FE.Models.Admin
{
    public class AdminReservationsViewModel
    {
        public IEnumerable<ReservationsDto> reservations;
        public string UserName { get; set; }
        public int UserId { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}

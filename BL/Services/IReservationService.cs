using BL.DTOs.Reservation;

namespace BL.Services
{
    public interface IReservationService
    {
        IEnumerable<ReservationsDto> getReservationsByUserId(int userId);
    }
}

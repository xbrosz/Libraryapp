using BL.Facades;

namespace FE.Models
{
    public class NewReservationModel
    {
        private readonly ReservationFacade _reservationFacade;

        public NewReservationModel(ReservationFacade reservationFacade)
        {
            _reservationFacade = reservationFacade;
        }
    }
}

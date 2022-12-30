using BL.DTOs.Reservation;
using BL.Facades.Facades;

namespace FE.Models
{
    public class NewReservationModel
    {
        public int BookID { get; set; }
        public IEnumerable<string> Branches { get; set; }
        public string BookTitle { get; set; }

    }
}

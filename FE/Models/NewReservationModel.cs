using BL.DTOs.Reservation;
using BL.Facades.Facades;

namespace FE.Models
{
    public class NewReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> Branches { get; set; }
        public string BookTitle { get; set; }
        public string SelectedBranch { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}

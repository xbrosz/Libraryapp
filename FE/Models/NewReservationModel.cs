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
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}

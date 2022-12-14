using BL.DTOs.Reservation;
using BL.Facades.Facades;

namespace FE.Models
{
    public class NewReservationModel
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int BranchID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BranchName { get; set; }
        public string BookTitle { get; set; }


        public ReservationCreateFormDto ToDto() => new()
        {
            BookId = BookID,
            BranchId = BranchID,
            StartDate = StartDate,
            EndDate = EndDate,
            UserId = UserID
        };
    }
}

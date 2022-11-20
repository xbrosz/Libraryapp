
namespace BL.DTOs.Reservation
{
    public class ReservationUpdateFormDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int BookPrintId { get; set; }
    }
}

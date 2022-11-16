namespace BL.DTOs.Reservation
{
    public class CreateReservationDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BranchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

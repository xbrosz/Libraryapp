namespace BL.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookPrintId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

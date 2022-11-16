namespace BL.DTOs.Reservation
{
    public class ReservationsDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BranchDto Branch { get; set; }

    }
}

namespace BL.DTOs.Reservation
{
    public class ReservationFilterDto
    {
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public int? BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

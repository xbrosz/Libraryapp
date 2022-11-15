namespace BL.DTOs
{
    public class ReservationFilterDto
    {
        public int UserId { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

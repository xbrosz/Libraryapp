namespace BL.DTOs
{
    public class RatingFilterDto
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

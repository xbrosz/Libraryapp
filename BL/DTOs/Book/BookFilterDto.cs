namespace BL.DTOs
{
    public class BookFilterDto
    {
        public int AuthorID { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

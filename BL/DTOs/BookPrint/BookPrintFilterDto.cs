namespace BL.DTOs
{
    public class BookPrintFilterDto
    {
        public int? BookId { get; set; }
        public int? BranchId { get; set; }
        public IEnumerable<int>? ReservedBookPrintIDs { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

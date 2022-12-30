using DAL.Entities;

namespace BL.DTOs
{
    public class BookFilterDto
    {
        public int? AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public double? LowestRating { get; set; }
        public string? Genre { get; set; }
        public string? Title { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; } = true;

    }
}

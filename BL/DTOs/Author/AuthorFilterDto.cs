namespace BL.DTOs.Author
{
    public class AuthorFilterDto
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}

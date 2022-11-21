namespace BL.DTOs.Branch
{
    public class BranchFilterDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}


namespace BL.DTOs.User
{
    public class UserFilterDto
    {
        public string name { get; set; }
        public bool exactName { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
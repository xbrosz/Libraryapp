namespace BL.DTOs.User
{
    public class UserFilterDto
    {
        public bool ExactUserName { get; set; } = true;
        public string UserName { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
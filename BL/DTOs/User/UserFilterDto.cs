namespace BL.DTOs.User
{
    public class UserFilterDto
    {
        public string Name { get; set; }
        public bool ExactName { get; set; }
        public string UserName { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
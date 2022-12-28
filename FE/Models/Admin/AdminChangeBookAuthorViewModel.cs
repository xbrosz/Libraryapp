using BL.DTOs.Author;

namespace FE.Models.Admin
{
    public class AdminChangeBookAuthorViewModel
    {
        public IEnumerable<AuthorGridDto> Authors { get; set; }
        public PaginationViewModel Pagination { get; set; }
        public int BookId { get; set; }
    }
}

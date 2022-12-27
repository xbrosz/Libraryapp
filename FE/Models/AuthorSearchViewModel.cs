using BL.DTOs.Author;

namespace FE.Models
{
    public class AuthorSearchViewModel
    {
        public IEnumerable<AuthorGridDto> Authors { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}

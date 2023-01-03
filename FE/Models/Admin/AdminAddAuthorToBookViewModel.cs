using BL.DTOs.Author;

namespace FE.Models.Admin
{
    public class AdminAddAuthorToBookViewModel
    {
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public List<AuthorGridDto> Authors { get; set; }
    }
}

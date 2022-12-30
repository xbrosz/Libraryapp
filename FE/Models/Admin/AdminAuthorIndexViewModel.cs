using BL.DTOs.Author;

namespace FE.Models.Admin
{
    public class AdminAuthorIndexViewModel
    {
        public List<Tuple<AuthorGridDto, bool>> Authors { get; set; }
    }
}

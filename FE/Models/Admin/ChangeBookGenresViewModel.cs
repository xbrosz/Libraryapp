using BL.DTOs.Genre;

namespace FE.Models.Admin
{
    public class ChangeBookGenresViewModel
    {
        public List<string> CheckedGenres { get; set; }
        public List<GenreDto> Genres { get; set; }
        public int BookId { get; set; }
    }
}

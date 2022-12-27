using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.BookGenre
{
    public class BookGenreFilterDto
    {
        public int? BookId { get; set; }
        public string? GenreName { get; set; }
        public int? GenreId { get; set; }
    }
}

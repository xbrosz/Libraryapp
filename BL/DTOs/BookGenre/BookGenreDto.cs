using BL.DTOs.Genre;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.BookGenre
{
    public class BookGenreDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public GenreDto Genre { get; set; }
    }
}

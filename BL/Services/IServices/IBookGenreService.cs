using BL.DTOs.BookGenre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IBookGenreService
    {
        void DeleteBookGenreForBookId(int bookId);

        void Insert(BookGenreDto dtoToInsert);

        void DeleteBookGenreForGenreId(int genreId);
    }
}

using BL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IBookFacade
    {
        IEnumerable<BookGridDto> GetBooksBySubstring(string substring);

        IEnumerable<Book> GetAllBooksSortedByRating();

        BookDetailDto GetBookDetailByID(int bookID);
    }
}

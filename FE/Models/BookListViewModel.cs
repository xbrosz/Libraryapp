using BL.DTOs;
using DAL.Entities;

namespace FE.Models
{
    public class BookListViewModel
    {
        public IEnumerable<BookGridDto> Books { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}

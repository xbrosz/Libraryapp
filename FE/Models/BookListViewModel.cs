using BL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FE.Models
{
    public class BookListViewModel
    {
        public IEnumerable<BookGridDto> Books { get; set; }
        public SelectList? Genres { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}

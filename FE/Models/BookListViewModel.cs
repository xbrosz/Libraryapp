using BL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FE.Models
{
    public class BookListViewModel
    {
        public IEnumerable<BookGridDto> Books { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? SearchString { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}

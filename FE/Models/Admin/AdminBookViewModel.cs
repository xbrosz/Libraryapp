using BL.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FE.Models.Admin
{
    public class AdminBookViewModel
    {
        public IEnumerable<BookGridDto> Books { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}

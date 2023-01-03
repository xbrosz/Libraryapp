using BL.DTOs.BookPrint;

namespace FE.Models.Admin
{
    public class AdminBookPrintIndexViewModel
    {
        public IEnumerable<BookPrintGridDto> bookPrints { get; set; }
    }
}

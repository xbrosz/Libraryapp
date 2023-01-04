using BL.DTOs.BookPrint;

namespace FE.Models.Admin
{
    public class AdminBookPrintIndexViewModel
    {
        public int Id { get; set; }
        public IEnumerable<BookPrintGridDto> bookPrints { get; set; }
    }
}

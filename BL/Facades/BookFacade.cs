using BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class BookFacade
    {
        private IBookService _bookService { get; set; }
        private IBookPrintService _bookPrintService { get; set; }

        public BookFacade(IBookService bookService, IBookPrintService bookPrintService)
        {
            _bookService = bookService;
            _bookPrintService = bookPrintService;
        }
    }
}

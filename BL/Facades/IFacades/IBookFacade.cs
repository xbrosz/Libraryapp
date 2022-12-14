using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IBookFacade
    {
        public IEnumerable<BookPrintDto> GetAvailableBookPrints(int bookId, int branchId, DateTime from, DateTime to);
        public int GetNumOfAvailablePrints(int bookId, int branchId, DateTime from, DateTime to);
        public BookDetailDto GetBookDetailByID(int bookID);
    }
}

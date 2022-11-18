using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IBookPrintService
    {
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId);
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId);
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId);
    }
}

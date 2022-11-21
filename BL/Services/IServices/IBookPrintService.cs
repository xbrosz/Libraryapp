using BL.DTOs;
using BL.DTOs.Reservation;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IBookPrintService : IGenericService<BookPrint, BookPrintDto, BookPrintDto, BookPrintDto>
    {
        public IEnumerable<BookPrintDto> GetBookPrintsByBranchIDAndBookID(int branchId, int bookId);
        public IEnumerable<BookPrintDto> GetBookPrintsByBranchID(int branchId);
        public IEnumerable<BookPrintDto> GetBookPrintsByBookID(int bookId);
        
    }
}

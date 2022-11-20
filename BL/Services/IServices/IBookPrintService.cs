using BL.DTOs;
using BL.DTOs.Reservation;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IBookPrintService : IGenericService<BookPrint, BookPrintDto, BookPrintDto, BookPrintDto>
    {
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId);
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId);
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId);
        
    }
}

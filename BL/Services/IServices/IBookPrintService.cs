using BL.DTOs;
using BL.DTOs.Reservation;


namespace BL.Services.IServices
{
    public interface IBookPrintService
    {
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId);
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId);
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId);
        
    }
}

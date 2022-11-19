using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.QueryObjects;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class BookPrintService : GenericService<BookPrint, BookPrintDto, BookPrintDto, BookPrintDto>, IBookPrintService
    {
        private IQueryObject<BookPrintFilterDto, BookPrintDto> _bookPrintQueryObject;

        public BookPrintService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookPrintFilterDto, BookPrintDto> bookPrintQueryObject) : base(unitOfWork, mapper, unitOfWork.BookPrintRepository)
        {
            _bookPrintQueryObject = bookPrintQueryObject;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId)
        {
            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId)
        {
            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId)
        {

            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
            }).Items;
        }

        public IEnumerable<BookPrintDto> GetAvailableBookPrints(IEnumerable<ReservationsDto> reservations)
        {
            throw new NotImplementedException();
        }
    }
}

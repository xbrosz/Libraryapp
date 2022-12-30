using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
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
        public IEnumerable<BookPrintDto> GetBookPrintsByBranchIDAndBookID(int branchId, int bookId)
        {
            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
                BranchId = branchId
            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookPrintsByBranchID(int branchId)
        {
            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BranchId = branchId
            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookPrintsByBookID(int bookId)
        {
            return _bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
            }).Items;
        }
    }
}

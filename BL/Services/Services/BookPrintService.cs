using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.QueryObjects;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;

namespace BL.Services.Services
{
    public class BookPrintService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private LibraryappDbContext _context;
        private IRepository<BookPrint> _bookPrintRepository;
        private BookPrintQueryObject bookPrintQueryObject;

        public BookPrintService(LibraryappDbContext context, IRepository<BookPrint> bookRepository)
        {
            _context = context;
            _bookPrintRepository = bookRepository;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId)
        {
            bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId)
        {
            bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {

                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId)
        {
            bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,


            }).Items;
        }
    }
}

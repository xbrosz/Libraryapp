using AutoMapper;
using BL.DTOs;
using BL.QueryObjects;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Repository;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
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
    }
}

using AutoMapper;
using BL.DTOs;
using BL.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Repository;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class BookPrintService : GenericService<BookPrint, BookPrintDto, BookPrintDto, BookPrintDto>, IBookPrintService
    {
        private LibraryappDbContext _context;

        public BookPrintService(LibraryappDbContext context, IRepository<BookPrint> bookRepository) : base(bookRepository)
        {
            _context = context;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchIDAndBookID(int branchId, int bookId)
        {
            var bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBranchID(int branchId)
        {
            var bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BranchId = branchId

            }).Items;
        }
        public IEnumerable<BookPrintDto> GetBookbyBookID(int bookId)
        {
            var bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
            }).Items;
        }

        public int GetNumberOfBooksAtBranch(int bookId, int branchId)
        {
            var bookPrintQueryObject = new BookPrintQueryObject(mapper, _context);

            return bookPrintQueryObject.ExecuteQuery(new BookPrintFilterDto
            {
                BookId = bookId,
                BranchId = branchId

            }).Items.Count();
        }
    }
}

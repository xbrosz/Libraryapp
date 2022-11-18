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
    public class BookService : GenericService<Book, BookDetailDto, BookPrintDto, BookDetailDto>, IBookService
    {
        private LibraryappDbContext _context;
        

        public BookService(LibraryappDbContext context, IRepository<Book> bookRepository) : base(bookRepository)
        {
            _context = context;

        }

        public IEnumerable<BookGridDto> GetBookbyAuthorID(int authorID)
        {
            var bookQueryObject = new BookQueryObject(mapper, _context);

            return bookQueryObject.ExecuteQuery(new BookFilterDto
            {
                AuthorID = authorID,

            }).Items;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return mapper.Map<BookDetailDto>(find(bookID));
        }

    }
}

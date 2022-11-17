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

namespace BL.Services.Services
{
    public class BookService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private LibraryappDbContext _context;
        private IRepository<Book> _bookRepository;
        private BookQueryObject bookQueryObject;

        public BookService(LibraryappDbContext context, IRepository<Book> bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public IEnumerable<BookGridDto> GetBookbyAuthorID(int authorID)
        {
            bookQueryObject = new BookQueryObject(mapper, _context);

            return bookQueryObject.ExecuteQuery(new BookFilterDto
            {
                AuthorID = authorID,

            }).Items;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return mapper.Map<BookDetailDto>(_bookRepository.GetByID(bookID));
        }

    }
}

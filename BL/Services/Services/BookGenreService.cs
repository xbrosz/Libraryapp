using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.DTOs.Genre;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class BookGenreService : GenericService<BookGenre, BookGenreDto, BookGenreDto, BookGenreDto>, IBookGenreService
    {
        private readonly IQueryObject<BookGenreFilterDto, BookGenreDto> _bookGenreQueryObject;

        public BookGenreService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookGenreFilterDto, BookGenreDto> bookGenreQueryObject) : base(unitOfWork, mapper, unitOfWork.BookGenreRepository)
        {
            _bookGenreQueryObject = bookGenreQueryObject;
        }

        public void DeleteBookGenreForBookId(int bookId)
        {
            foreach(var bookGenre in _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = bookId }).Items)
            {
                Delete(bookGenre.Id);
            }
        }

        public void DeleteBookGenreForGenreId(int genreId)
        {
            foreach (var bookGenre in _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { GenreId = genreId }).Items)
            {
                Delete(bookGenre.Id);
            }
        }
    }
}

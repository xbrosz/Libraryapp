﻿using AutoMapper;
using BL.DTOs;
using BL.DTOs.Book;
using BL.DTOs.BookGenre;
using BL.DTOs.Genre;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using System.Drawing.Printing;

namespace BL.Services.Services
{
    public class BookService : GenericService<Book, BookDetailDto, BookUpdateDto, BookInsertDto>, IBookService
    {
        private readonly IQueryObject<BookFilterDto, BookGridDto> _bookQueryObject;
        private readonly IQueryObject<BookGenreFilterDto, BookGenreDto> _bookGenreQueryObject;
        private readonly IQueryObject<GenreDto, GenreDto> _genreQueryObject;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookFilterDto, BookGridDto> bookQueryObject, IQueryObject<BookGenreFilterDto, BookGenreDto> bookGenreQueryObject, IQueryObject<GenreDto, GenreDto> genreQueryObject) : base(unitOfWork, mapper, unitOfWork.BookRepository)
        {
            _bookQueryObject = bookQueryObject;
            _bookGenreQueryObject = bookGenreQueryObject;
            _genreQueryObject = genreQueryObject;
        }

        private IEnumerable<BookGridDto> AddGenresToBooks(IEnumerable<BookGridDto> books)
        {
            foreach (var book in books)
            {
                book.BookGenres = string.Join("/", _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = book.Id}).Items.Select(x => x.Genre.Name));
            }

            return books;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            var book = Find(bookID);
            book.BookGenres = string.Join("/", _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = bookID }).Items.Select(x => x.Genre.Name));
            return book;
        }

        public IEnumerable<BookGridDto> GetBooksbyFilter(BookFilterDto filter)
        {
            List<BookGridDto> books;

            if (!string.IsNullOrWhiteSpace(filter.Genre))
            {
                var genreId = _genreQueryObject.ExecuteQuery(new GenreDto() { Name = filter.Genre }).Items.First().Id;
                var booksWithSpecifiedGenre = _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { GenreId = genreId }).Items.Select(x => x.BookId).ToList() ;
                books = _bookQueryObject.ExecuteQuery(filter).Items.Where(x => booksWithSpecifiedGenre.Contains(x.Id)).ToList();
            } else
            {
                books = _bookQueryObject.ExecuteQuery(filter).Items.ToList();
            }

            return AddGenresToBooks(books);
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            return _mapper.Map<IEnumerable<GenreDto>>(_unitOfWork.GenreRepository.GetAll());
        }

        public IEnumerable<BookGridDto> GetAllBooks() 
        {
            return AddGenresToBooks(_mapper.Map<IEnumerable<BookGridDto>>(_unitOfWork.BookRepository.GetAll()));
        }

        public void UpdateBook(BookUpdateDto dto) 
        {
            var book = _unitOfWork.BookRepository.GetByID(dto.Id);

            if (!dto.Release.HasValue)
            {
                dto.Release = book.Release;
            }

            if (!dto.AuthorId.HasValue)
            {
                dto.AuthorId = book.AuthorId;
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                dto.Title = book.Title;
            }

            if (!dto.RatingNumber.HasValue && !dto.NoRating)
            {
                dto.RatingNumber = book.RatingNumber;
            }

            if (dto.RatingNumber.HasValue)
            {
                dto.SortRatingNumber = dto.RatingNumber.Value;
            }

            Update(dto);
        }

        
    }
}

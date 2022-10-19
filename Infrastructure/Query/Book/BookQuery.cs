using DAL.Data;
using Infrastructure.DTOs.Book;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Query.Book
{
    public class BookQuery : IBookQuery
    {
        private LibraryappDbContext _context;

        public BookQuery(LibraryappDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookDTO>> GetAll()
        {
            var dtos = await (from b in _context.Book.Include(b => b.Author)
                                                .Include(b => b.Ratings)
                                                .Include(b => b.BookPrints)
                         select new BookDTO
                         {
                             ID = b.Id,
                             Author = b.Author.FirstName + " " + b.Author.LastName,
                             AverageRating = b.Ratings.Count == 0 ? null : b.Ratings.Select(r => r.RatingNumber).Average(),
                             NumberOfPrints = b.BookPrints.Count,
                             Genres = (from g in _context.BookGenre
                                       where g.BookId == b.Id
                                       select g.Genre.Name).ToList(),
                             Title = b.Title,
                             ReleaseDate = b.Release
                         }).ToListAsync();
            return dtos;
        }

        public async Task<BookDTO> GetByID(int ID)
        {
            var dto = (from b in _context.Book
                       where b.Id == ID
                        select new BookDTO
                        {
                            ID = b.Id,
                            Author = b.Author.FirstName + " " + b.Author.LastName,
                            Genres = (from g in _context.BookGenre
                                      where g.BookId == ID
                                      select g.Genre.Name).ToList(),
                            AverageRating = b.Ratings.Count == 0 ? null : b.Ratings.Select(r => r.RatingNumber).Average(),
                            NumberOfPrints = (from p in _context.BookPrint
                                              where p.BookId == b.Id
                                              select p.Id).Count(),
                            Title = b.Title,
                            ReleaseDate = b.Release

                        }).SingleOrDefault();
            if (dto == null)
            {
                throw new KeyNotFoundException("Book with given ID was not found");
            }
            return await Task.FromResult(dto);
        }
    }
}

using AutoMapper;
using BL.DTOs;
using BL.QueryObjects;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class RatingService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private LibraryappDbContext _context;
        private IRepository<Rating> _ratingRepository;
        private IRepository<Book> _bookRepository;

        private RatingQueryObject ratingQueryObject;


        public double GetBookAverageRating(int bookId)
        {
            ratingQueryObject = new RatingQueryObject(mapper, _context);

            return ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            }).Items.Select(r => r.RatingNumber).Average();
        }

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId)
        {
            ratingQueryObject = new RatingQueryObject(mapper, _context);

            return ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            }).Items;
        }
    }
}

using AutoMapper;
using BL.QueryObjects;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
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
            return _context.Rating.Select(r => r.RatingNumber).Average();
        }
    }
}

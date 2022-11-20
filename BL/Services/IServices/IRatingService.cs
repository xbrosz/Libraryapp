using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IRatingService
    {
        public double GetBookAverageRating(int bookId);

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId);



    }
}

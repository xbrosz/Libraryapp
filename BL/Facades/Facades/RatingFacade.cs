using BL.DTOs;
using BL.DTOs.Rating;
using BL.Facades.IFacades;
using BL.QueryObjects.QueryObjects;
using BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.Facades
{
    public class RatingFacade : IRatingFacade
    {
        private readonly IRatingService _ratingService;
        private readonly IReservationService _reservationService;
        private readonly IBookService _bookService;

        public RatingFacade(IRatingService ratingService, IReservationService reservationService, IBookService bookService)
        {
            _ratingService = ratingService;
            _reservationService = reservationService;
            _bookService = bookService;
        }

        public IEnumerable<RatingAwaitingDto> GetAwaitingRatingsByUser(int userId)
        {
            var ratings = _ratingService.GetRatingsByUser(userId);
            var reservations = _reservationService.GetReservationsByUserId(userId);

            var awaitingRatings = reservations
                .Where(res => res.EndDate.Date < DateTime.Today.Date
                    && !ratings.Any(rat => rat.BookId == res.BookId)
                )
                .DistinctBy(res => res.BookId)
                .Select(res => new RatingAwaitingDto { BookId = res.BookId, BookTitle = res.BookTitle });

            return awaitingRatings;
        }

        public void InsertRating(RatingDto rating) 
        {
            _ratingService.Insert(rating);
            var newRatingNumber = _ratingService.GetBookAverageRating(rating.BookId);
            _bookService.UpdateBook(new BookUpdateDto() { Id = rating.BookId, RatingNumber = newRatingNumber, NoRating = newRatingNumber == null ? true : false });
        }

        public void UpdateRating(RatingDto rating)
        {
            _ratingService.Update(rating);
            var newRatingNumber = _ratingService.GetBookAverageRating(rating.BookId);
            _bookService.UpdateBook(new BookUpdateDto() { RatingNumber = newRatingNumber, NoRating = newRatingNumber == null ? true : false, Id = rating.BookId });
        }

        public void DeleteRating(RatingDto rating)
        {
            _ratingService.Delete(rating.Id);
            var newRatingNumber = _ratingService.GetBookAverageRating(rating.BookId);
            _bookService.UpdateBook(new BookUpdateDto() { RatingNumber = newRatingNumber, NoRating = newRatingNumber == null ? true : false, Id = rating.BookId });
        }
    }
}

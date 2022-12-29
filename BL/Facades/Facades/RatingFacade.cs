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

        public RatingFacade(IRatingService ratingService, IReservationService reservationService)
        {
            _ratingService = ratingService;
            _reservationService = reservationService;
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
    }
}

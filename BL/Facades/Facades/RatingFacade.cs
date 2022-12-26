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

            var awaitingRatings = reservations.DistinctBy(res => res.BookId)
                .Where(res => res.EndDate < DateTime.Today
                    && !ratings.Any(rat => rat.BookId == res.BookId)
                )
                .Select(res => new RatingAwaitingDto { BookId = res.BookId, BookTitle = res.BookTitle });

            return awaitingRatings;
        }
    }
}

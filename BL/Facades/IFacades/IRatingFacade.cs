using BL.DTOs;
using BL.DTOs.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IRatingFacade
    {
        public IEnumerable<RatingAwaitingDto> GetAwaitingRatingsByUser(int userId);
    }
}

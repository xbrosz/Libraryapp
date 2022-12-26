using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Rating
{
    public class RatingAwaitingDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
    }
}

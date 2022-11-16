using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int RatingNumber { get; set; }
        public string Comment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class RatingFilterDto
    {
        public int BookId { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

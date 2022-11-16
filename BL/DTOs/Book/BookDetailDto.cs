using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class BookDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public string AuthorName { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public List<string> BookGenres { get; set; }

    }
}

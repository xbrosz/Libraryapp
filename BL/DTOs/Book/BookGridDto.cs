using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class BookGridDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public string AuthorName { get; set; }
        public double Rating { get; set; }
        public string BookGenres { get; set; }
    }
}

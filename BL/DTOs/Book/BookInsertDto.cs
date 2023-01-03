using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Book
{
    public class BookInsertDto
    {
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int AuthorId { get; set; }
    }
}

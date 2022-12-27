using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class BookAuthor : BaseEntity
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string AuthorName { get; set; }
    }
}

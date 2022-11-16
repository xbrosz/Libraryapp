﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class BookEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int AuthorId { get; set; }
        public List<int> Ratings { get; set; }
        public List<int> BookGenres { get; set; }
    }
}
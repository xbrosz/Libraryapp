﻿using BL.DTOs.Author;

namespace FE.Models.Admin
{
    public class ChangeBookAuthorViewModel
    {
        public IEnumerable<AuthorGridDto> Authors { get; set; }
        public int BookId { get; set; }
    }
}

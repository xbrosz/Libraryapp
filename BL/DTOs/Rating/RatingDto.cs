﻿namespace BL.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int RatingNumber { get; set; }
        public string Comment { get; set; }
    }
}

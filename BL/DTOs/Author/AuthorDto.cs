using System.ComponentModel.DataAnnotations;

namespace BL.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

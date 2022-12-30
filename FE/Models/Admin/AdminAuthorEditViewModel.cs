using System.ComponentModel.DataAnnotations;

namespace FE.Models.Admin
{
    public class AdminAuthorEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}

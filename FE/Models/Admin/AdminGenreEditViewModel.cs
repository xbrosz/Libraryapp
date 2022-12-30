using System.ComponentModel.DataAnnotations;

namespace FE.Models.Admin
{
    public class AdminGenreEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

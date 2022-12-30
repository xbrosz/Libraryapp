using System.ComponentModel.DataAnnotations;

namespace FE.Models.Admin
{
    public class AdminGenreAddViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}

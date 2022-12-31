using System.ComponentModel.DataAnnotations;

namespace FE.Models.Admin
{
    public class AdminBranchEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

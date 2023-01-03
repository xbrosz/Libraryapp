using System.ComponentModel.DataAnnotations;

namespace FE.Models.Admin
{
    public class AdminBranchAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

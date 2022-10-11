using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Branch : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
    }
}

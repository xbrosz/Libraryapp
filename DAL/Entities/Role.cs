using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Role : BaseEntity
    {
        [MaxLength(60)]
        public string Name { get; set; }
    }
}

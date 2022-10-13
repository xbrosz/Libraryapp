using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Genre : BaseEntity
    {
        [MaxLength(120)]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Author : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

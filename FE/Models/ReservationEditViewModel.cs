using BL.DTOs.Branch;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FE.Models
{
    public class ReservationEditViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int BookPrintId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int BranchId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (EndDate < StartDate)
            {
                errors.Add(new ValidationResult($"{nameof(EndDate)} needs to be greater than Start date.", new List<string> { nameof(EndDate) }));
            }

            if (EndDate <= DateTime.Today)
            {
                errors.Add(new ValidationResult($"{nameof(EndDate)} needs to be greater than Today.", new List<string> { nameof(EndDate) }));
            }

            return errors;
        }
    }
}

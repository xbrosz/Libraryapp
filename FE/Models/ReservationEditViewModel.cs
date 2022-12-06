using BL.DTOs.Branch;
using System.ComponentModel.DataAnnotations;

namespace FE.Models
{
    public class ReservationEditViewModel
    {
        public int Id { get; set; }
        [Editable(false)]
        public string BookTitle { get; set; }
        public int BookPrintId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public BranchDto Branch { get; set; }
    }
}

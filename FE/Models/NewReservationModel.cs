using BL.DTOs.Reservation;
using BL.Facades.Facades;
using System.ComponentModel.DataAnnotations;

namespace FE.Models
{
    public class NewReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> Branches { get; set; }
        public string BookTitle { get; set; }
        public string SelectedBranch { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public string FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public string ToDate { get; set; }

    }
}

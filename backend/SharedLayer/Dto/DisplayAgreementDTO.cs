using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dto
{
    public class DisplayAgreementDTO
    {
        public int AgreementId { get; set; }
        [Required(ErrorMessage = "UserId is Required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "CarId is Required")]
        public int CarId { get; set; }
        [Required(ErrorMessage = "RentalDuration is Required")]
        public int RentalDuration { get; set; }
        [Required(ErrorMessage = "TotalCost is Required")]
        public int TotalCost { get; set; }
        [Required(ErrorMessage = "IsAccepted is Required")]
        public bool IsAccepted { get; set; }
        [Required(ErrorMessage = "IsReturnedRequested is Required")]
        public bool IsReturnRequested { get; set; }
        [Required(ErrorMessage = "IsReturned is Required")]
        public bool IsReturned { get; set; }
        [Required(ErrorMessage = "From Date is required")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime ToDate { get; set; }
    }
}

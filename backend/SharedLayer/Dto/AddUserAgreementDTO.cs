using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dto
{
    //take from frontend
    public class AddUserAgreementDTO
    {
        [Required(ErrorMessage = "CarId is Required")]
        public int CarId { get; set; }
        [Required(ErrorMessage = "RentalDuration is Required")]
        public int RentalDuration { get; set; }
    }
}

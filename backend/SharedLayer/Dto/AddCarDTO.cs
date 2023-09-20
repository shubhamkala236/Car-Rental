using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Dto
{
    public class AddCarDTO
    {

        [Required(ErrorMessage = "Maker is Required")]
        public string Maker { get; set; }
        [Required(ErrorMessage = "Model is Required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Rent Price is Required")]
        public int RentPrice { get; set; }
        [Required(ErrorMessage = "Status is Required")]
        public string AvailibilityStatus { get; set; }
    }
}

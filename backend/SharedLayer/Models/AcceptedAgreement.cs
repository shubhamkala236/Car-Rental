using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Models
{
    public class AcceptedAgreement
    {
        [Key]
        public int AcceptedAgreementId { get; set; }

        [Required(ErrorMessage = "AgreementId is Required")]
        public int AgreementId { get; set; }
        [Required(ErrorMessage = "DateTime is required")]
        public DateTime AcceptDate { get; set; }

    }
}

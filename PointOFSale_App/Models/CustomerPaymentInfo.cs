using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.Models
{
    public class CustomerPayment
    {
        [Key]
        public int CustomerPaymentId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Payment Date is required")]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Required]
        public double PayAmount { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PointOFSale_App.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        [Required]
        public int VoucherNumber { get; set; }
        [Required]
        public double  PayMoney { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Payment Date is required")]
        [Display(Name = "Payment Date")]
        public DateTime PayDate { get; set; }
        [StringLength(50,ErrorMessage ="Maximum size should be 50")]
        [Required]
        public string PayTo { get; set; }  
        [StringLength(50, ErrorMessage = "Maximum size should be 50")]
        [Required]
        public int EmployeeId { get; set; }   //cashier

        public virtual Employee Employee { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PointOFSale_App.Models
{
    public class ProductEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductEntryId { get; set; }
        [Required]
        public int VoucherNumber { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Discount")]
        public double Discount_Percentage { get; set; }
        [Required]
        public double SubTotalCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Of Entry is required")]
        [Display(Name = "Date Of Entry")]
        public DateTime DateOfEntry { get; set; }

        //ForeignKey
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("EmployeeId")]
        [Display(Name = "Receiption Officer")]
        public int EmployeeId { get; set; }


        public virtual Category Category { get; set; }
        public virtual Employee Employee { get; set; }


    }
}

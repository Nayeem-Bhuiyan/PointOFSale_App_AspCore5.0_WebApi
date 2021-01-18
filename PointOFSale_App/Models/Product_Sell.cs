using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.Models
{
    public class Product_Sell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_SellId { get; set; }
        public double Quantity { get; set; }
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Discount")]
        public double Discount_Percentage { get; set; }
        public double SubTotalCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Of Sell is required")]
        [Display(Name = "Date Of Sell")]
        public DateTime DateOfSell { get; set; }

        //ForeignKey
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("EmployeeId")]
        [Display(Name = "Sell Officer")]
        public int EmployeeId { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }


        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

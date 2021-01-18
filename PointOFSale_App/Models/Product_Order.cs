using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PointOFSale_App.Models
{
    public class ProductOrder
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOrderId { get; set; }
        [Required]
        public double Quantity { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Company Contact")]
        public string CompanyContactNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "DateOfOrder is required")]
        [Display(Name = "Order Date")]
        public DateTime Orderdate { get; set; }
        [Column(TypeName ="date")]
        public DateTime Date_OrderSupply { get; set; }
       
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}

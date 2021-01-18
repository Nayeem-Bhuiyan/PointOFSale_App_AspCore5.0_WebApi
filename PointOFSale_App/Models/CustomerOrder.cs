using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace PointOFSale_App.Models
{
    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerOrderId { get; set; }
        public double Quantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "DateOfOrder is required")]
        [Display(Name = "Order Date")]
        public DateTime DateOfOrder { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Category Category { get; set; }
    }
}

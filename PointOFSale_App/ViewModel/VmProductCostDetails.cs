using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOFSale_App.ViewModel
{
    public class VmProductCostDetails
    {
        public int VmProductCostDetailsId { get; set; }
        public int VoucherNumber { get; set; }
        public int ProductEntryId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount_Percentage { get; set; }
        public double SubTotalCost { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEntry { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
    }
}

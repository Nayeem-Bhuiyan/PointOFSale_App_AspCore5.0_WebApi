using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmCustomerBillAndPayment
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CustomerMobileNo { get; set; }

        public double  TotalBill { get; set; }
        public double  TotalPayment { get; set; }
        public double  DueOfPayment { get; set; }

        public string PaymentDate { get; set; }
        public string SellDate { get; set; }
        
    }
}

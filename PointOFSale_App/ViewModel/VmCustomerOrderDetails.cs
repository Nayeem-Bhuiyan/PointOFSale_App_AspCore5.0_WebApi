using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmCustomerOrderDetails
    {
        public int CustomerOrderId { get; set; }
        public double Quantity { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryStatus { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CustomerMobileNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmPaymentDetails
    {
        //public int PaymentId { get; set; }
        //public double PayMoney { get; set; }
        //public DateTime PayDate { get; set; }
        //public string PayTo { get; set; }

        //
        public int VoucherNumber { get; set; }
        public double TotalBill { get; set; }
        public double TotalPayBill { get; set; }
        public double TotalDueBill { get; set; }
        public string PaymentStatus { get; set; }

        //
        public string[] PayClientNames { get; set; }
        public string[] AllPayDates { get; set; }

    }
}

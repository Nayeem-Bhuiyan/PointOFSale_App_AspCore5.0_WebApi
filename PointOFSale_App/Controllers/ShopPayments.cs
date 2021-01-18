using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOFSale_App.Models;
using Microsoft.EntityFrameworkCore;
using PointOFSale_App.ViewModel;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopPayments : ControllerBase
    {
        private readonly PointOfSellDbContext db;

        public ShopPayments(PointOfSellDbContext _db)
        {
            this.db = _db;
        }



        public async Task<ActionResult<VmPaymentDetails>> BillPaymentsByVoucherNumber(int VoucherNumber)
        {
            double TotalBillOfThisVoucher = 0;
            double TotalPaymentsOfThisVoucher = 0;
            double TotalDueOfThisVoucher = 0;
            string[] allPayDates = null;
            string[] payToClientNames = null;
            int i = 0;

            string payStatus =null;

            var ProductCostList   = await db.ProductEntry.Where(pe=>pe.VoucherNumber==VoucherNumber).ToListAsync();

            foreach (var data in ProductCostList)
            {
                TotalBillOfThisVoucher += data.SubTotalCost;
                
            }

          var PaymentList = await db.Payment.Where(p => p.VoucherNumber == VoucherNumber).ToListAsync();

            foreach (var item in PaymentList)
            {
                TotalPaymentsOfThisVoucher += item.PayMoney;
                allPayDates[i]= Convert.ToString(item.PayDate);
                payToClientNames[i]= Convert.ToString(item.PayTo);
                i++;
            }

            TotalDueOfThisVoucher = Convert.ToDouble(TotalBillOfThisVoucher - TotalPaymentsOfThisVoucher);

            if (TotalBillOfThisVoucher> TotalPaymentsOfThisVoucher)
            {
                payStatus = "Due";
            }
            else if(TotalBillOfThisVoucher == TotalPaymentsOfThisVoucher)
                {
                payStatus = "Full Paid";
            }
            else
            {
                payStatus = "Invalid Payments";
            }

            VmPaymentDetails PaymentDetails = new VmPaymentDetails()
            {



             VoucherNumber =VoucherNumber ,
             TotalBill =TotalBillOfThisVoucher ,
             TotalPayBill =TotalPaymentsOfThisVoucher ,
             TotalDueBill =TotalDueOfThisVoucher ,
             PaymentStatus =payStatus ,
             PayClientNames = payToClientNames,
             AllPayDates = allPayDates,
    };



            return PaymentDetails;

        }










    }
}

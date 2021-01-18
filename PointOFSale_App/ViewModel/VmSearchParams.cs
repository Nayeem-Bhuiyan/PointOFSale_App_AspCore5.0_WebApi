using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmSearchParams
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CustomerMobileNo { get; set; }

        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public string EmpMobileNo { get; set; }
        public string EmpAddress { get; set; }


        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int ThanaId { get; set; }
        public string ThanaName { get; set; }

        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public int ReligionId { get; set; }
        public string ReligionName { get; set; }

        public int CustomerPaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PayAmount { get; set; }

        public int ProductEntryId { get; set; }
        public int VoucherNumber { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount_Percentage { get; set; }
        public double SubTotalCost { get; set; }
        public DateTime DateOfEntry { get; set; }

        public int Product_SellId { get; set; }
        public DateTime DateOfSell { get; set; }

        public int ProductOrderId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactNumber { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime Date_OrderSupply { get; set; }

        public int PaymentId { get; set; }
        public double PayMoney { get; set; }
        public DateTime PayDate { get; set; }
        public string PayTo { get; set; }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointOFSale_App.Models;
using PointOFSale_App.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerPayments : ControllerBase
    {
        private readonly PointOfSellDbContext db;

        public CustomerPayments(PointOfSellDbContext _db)
        {
            this.db = _db;
        }

        [HttpGet]
        [ActionName("Today_TotalProductCost")]
        public async Task<ActionResult<double>> Today_TotalProductCost()
        {
            double Today_SumOfProductCost = 0;
            var today_ProductBuyingList = await db.ProductEntry.Where(cp => cp.DateOfEntry.Year == DateTime.Now.Year && cp.DateOfEntry.Month == DateTime.Now.Month && cp.DateOfEntry.Day == DateTime.Now.Day).ToListAsync();
            foreach (var item in today_ProductBuyingList)
            {
                Today_SumOfProductCost += item.SubTotalCost;
            }
            return Today_SumOfProductCost;
        }

        [HttpGet]
        [ActionName("Today_TotalCashAmount")]
        public async Task<ActionResult<double>> Today_TotalCashAmount()
        {
            double Today_TotalDeposit = 0;
          var todayPaymentList  =await db.CustomerPayment.Where(cp=> cp.PaymentDate.Year == DateTime.Now.Year && cp.PaymentDate.Month == DateTime.Now.Month && cp.PaymentDate.Day == DateTime.Now.Day).ToListAsync();
            foreach (var item in todayPaymentList)
            {
                Today_TotalDeposit += item.PayAmount;
            }
            return Today_TotalDeposit;
        }

        [HttpGet]
        [ActionName("Today_TotalSellAmount")]
        public async Task<ActionResult<double>> Today_TotalSellAmount()
        {
            double Today_TotalSellAmount = 0;
            var todaySellProductList = await db.Product_Sell.Where(cp => cp.DateOfSell.Year == DateTime.Now.Year && cp.DateOfSell.Month == DateTime.Now.Month && cp.DateOfSell.Day == DateTime.Now.Day).ToListAsync();
            foreach (var item in todaySellProductList)
            {
                Today_TotalSellAmount += item.SubTotalCost;
            }
            return Today_TotalSellAmount;
        }



        [HttpGet]
        [ActionName("DuePayment_CustomerBillAndPaymentList")]
        public async Task<ActionResult<IEnumerable<VmCustomerBillAndPayment>>> DuePayment_CustomerBillAndPaymentList()
        {
            List<VmCustomerBillAndPayment> vmList = new List<VmCustomerBillAndPayment>();

            double totalPaymentCount = 0;
            double totalBillCount = 0;
            double duePaymentCount = 0;

            var ProductBuyerList = await db.Product_Sell.ToListAsync();

            var customerList = await db.Customer.ToListAsync();

            foreach (var data in customerList)
            {


                var customerPayments = await db.CustomerPayment.Where(cp => cp.CustomerId == data.CustomerId).Select(p => new { p.CustomerId, p.CustomerPaymentId, p.PayAmount, p.PaymentDate }).ToListAsync();

                if (customerPayments != null)
                {
                    foreach (var item in customerPayments)
                    {

                        totalPaymentCount += item.PayAmount;

                    }
                }

                var SpecificCustomerBuyingList = await db.Product_Sell.Where(c => c.CustomerId == data.CustomerId).Select(s => new { s.CategoryId, s.CustomerId, s.DateOfSell, s.Discount_Percentage, s.EmployeeId, s.Product_SellId, s.Quantity, s.SubTotalCost, s.UnitPrice }).ToListAsync();

                if (SpecificCustomerBuyingList != null)
                {
                    foreach (var item in SpecificCustomerBuyingList)
                    {
                        totalBillCount += item.SubTotalCost;
                    }
                }


                duePaymentCount = Convert.ToDouble(totalBillCount - totalPaymentCount);


                var customer = await db.Customer.Where(c => c.CustomerId == data.CustomerId).Select(s => new { s.Address, s.CustomerId, s.CustomerMobileNo, s.CustomerName }).FirstOrDefaultAsync();

                if (customer != null && duePaymentCount > 0)
                {
                    VmCustomerBillAndPayment vm = new VmCustomerBillAndPayment()
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        Address = customer.Address,
                        CustomerMobileNo = customer.CustomerMobileNo,
                        TotalBill = totalBillCount,
                        TotalPayment = totalPaymentCount,
                        DueOfPayment = duePaymentCount
                    };

                    vmList.Add(vm);

                    totalPaymentCount = 0;
                    totalBillCount = 0;
                    duePaymentCount = 0;

                }


            }

            return vmList;
        }










        //Search specific Customer bill and payment informations

        [HttpPost]
        [ActionName("CustomerBillAndPaymentDetails")]
        public async Task<ActionResult<VmCustomerBillAndPayment>> CustomerBillAndPaymentDetails(VmSearchParams cm)
        {
            double totalPaymentCount = 0;
            double totalBillCount = 0;
            double duePaymentCount = 0;
            Customer customer = new Customer();

            if (cm.CustomerId > 0 )
            {
                var data = await db.Customer.Where(c => c.CustomerId == cm.CustomerId).Select(e => new { e.Address, e.CustomerId, e.CustomerMobileNo, e.CustomerName }).FirstOrDefaultAsync();
                Customer cst = new Customer()
                {
                    CustomerId = data.CustomerId,
                    CustomerMobileNo = data.CustomerMobileNo,
                    CustomerName = data.CustomerName,
                    Address = data.Address
                };
                customer = null;
                customer = cst;
            }
           else if (cm.CustomerMobileNo!=null)
            {
                 var data = await db.Customer.Where(c => c.CustomerMobileNo == cm.CustomerMobileNo).Select(e => new { e.Address, e.CustomerId, e.CustomerMobileNo, e.CustomerName }).FirstOrDefaultAsync();
                Customer cst = new Customer()
                {
                    CustomerId = data.CustomerId,
                    CustomerMobileNo = data.CustomerMobileNo,
                    CustomerName = data.CustomerName,
                    Address = data.Address
                };
                customer = null;
                customer = cst;
            }
            else if (cm.CustomerName != null)
            {
                var data = await db.Customer.Where(c => c.CustomerName.ToLower() == cm.CustomerName.ToLower()).Select(e => new { e.Address, e.CustomerId, e.CustomerMobileNo, e.CustomerName }).FirstOrDefaultAsync();
                Customer cst = new Customer()
                {
                    CustomerId = data.CustomerId,
                    CustomerMobileNo = data.CustomerMobileNo,
                    CustomerName = data.CustomerName,
                    Address = data.Address
                };
                customer = null;
                customer = cst;
            }
            else if (cm.Address != null)
            {
                var data = await db.Customer.Where(c => c.Address.ToLower() == cm.Address.ToLower()).Select(e => new { e.Address, e.CustomerId, e.CustomerMobileNo, e.CustomerName }).FirstOrDefaultAsync();
                Customer cst = new Customer()
                {
                    CustomerId = data.CustomerId,
                    CustomerMobileNo = data.CustomerMobileNo,
                    CustomerName = data.CustomerName,
                    Address = data.Address
                };
                customer = null;
                customer = cst;
            }
            


            var customerPayments=  await db.CustomerPayment.Where(cp => cp.CustomerId == customer.CustomerId).Select(p => new { p.CustomerId, p.CustomerPaymentId, p.PayAmount, p.PaymentDate }).ToListAsync();

            foreach (var item in customerPayments)
            {
                totalPaymentCount += item.PayAmount;
            }

            var customerBuyingList = await db.Product_Sell.Where(c => c.CustomerId == customer.CustomerId).Select(s => new { s.CategoryId, s.CustomerId, s.DateOfSell, s.Discount_Percentage, s.EmployeeId, s.Product_SellId, s.Quantity, s.SubTotalCost, s.UnitPrice }).ToListAsync();

            foreach (var item in customerBuyingList)
            {
                totalBillCount += item.SubTotalCost;
            }

            duePaymentCount = Convert.ToDouble(totalBillCount - totalPaymentCount);

            VmCustomerBillAndPayment vm = new VmCustomerBillAndPayment()
            {
                CustomerId = customer.CustomerId,
                CustomerName =customer.CustomerName ,
                Address = customer.Address,
                CustomerMobileNo =customer.CustomerMobileNo ,
                TotalBill =totalBillCount ,
                TotalPayment = totalPaymentCount,
                DueOfPayment = duePaymentCount
            };


            return vm;
        }
        
       
        //Retrieve All Customers Bill and payment informations 

        [HttpGet]
        [ActionName("CustomerBillAndPaymentList")]
        public async Task<ActionResult<IEnumerable<VmCustomerBillAndPayment>>> CustomerBillAndPaymentList()
        {
            List<VmCustomerBillAndPayment> vmList = new List<VmCustomerBillAndPayment>();
            
            double totalPaymentCount = 0;
            double totalBillCount = 0;
            double duePaymentCount = 0;
           

            var ProductBuyerList = await db.Product_Sell.ToListAsync();

            var customerList = await db.Customer.ToListAsync();

            foreach (var data in customerList)
            {


                var customerPayments = await db.CustomerPayment.Where(cp => cp.CustomerId == data.CustomerId).Select(p => new { p.CustomerId, p.CustomerPaymentId, p.PayAmount, p.PaymentDate }).ToListAsync();

                if (customerPayments!=null)
                {
                    foreach (var item in customerPayments)
                    {

                        totalPaymentCount += item.PayAmount;

                    }
                }

                var SpecificCustomerBuyingList = await db.Product_Sell.Where(c => c.CustomerId == data.CustomerId).Select(s => new { s.CategoryId, s.CustomerId, s.DateOfSell, s.Discount_Percentage, s.EmployeeId, s.Product_SellId, s.Quantity, s.SubTotalCost, s.UnitPrice }).ToListAsync();

                if (SpecificCustomerBuyingList!=null)
                {
                    foreach (var item in SpecificCustomerBuyingList)
                    {
                        totalBillCount += item.SubTotalCost;
                    }
                }

            
                    duePaymentCount = Convert.ToDouble(totalBillCount - totalPaymentCount);
                

                var customer = await db.Customer.Where(c => c.CustomerId == data.CustomerId).Select(s => new { s.Address, s.CustomerId, s.CustomerMobileNo, s.CustomerName }).FirstOrDefaultAsync();

                if (customer!=null && totalBillCount>0)
                {
                    VmCustomerBillAndPayment vm = new VmCustomerBillAndPayment()
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        Address = customer.Address,
                        CustomerMobileNo = customer.CustomerMobileNo,
                        TotalBill = totalBillCount,
                        TotalPayment = totalPaymentCount,
                        DueOfPayment = duePaymentCount
                    };

                    vmList.Add(vm);

                     totalPaymentCount = 0;
                     totalBillCount = 0;
                     duePaymentCount = 0;

                }

               
            }

            return vmList;
        }






    }
}

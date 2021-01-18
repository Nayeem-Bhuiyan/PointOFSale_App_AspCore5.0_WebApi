using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOFSale_App.Models;
using PointOFSale_App.ViewModel;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Product_SellController : ControllerBase
    {
        private readonly PointOfSellDbContext _db;

        public Product_SellController(PointOfSellDbContext db)
        {
            _db = db;
        }


        //After Change of Brand Id this event will fire
        // GET: api/Product_Sell/GetBrandWiseProductList/5
        [HttpGet("{id}")]
        [ActionName("GetBrandWiseProductList")]
        public async Task<ActionResult<IEnumerable<VmCategoryAndStockProduct>>> GetBrandWiseProductList(int id) //id=brandId
        {
            List<VmCategoryAndStockProduct> vmList = new List<VmCategoryAndStockProduct>();
            
            var produCtcategoryList= await _db.Category.Where(c=>c.BrandId==id).ToListAsync();
            double SellUnitPrice =0;
            double stockAvailabe = 0;
           


            foreach (var item in produCtcategoryList)
            {

                var brand = await _db.Brand.Where(b => b.BrandId == item.BrandId).FirstOrDefaultAsync();
                var product = await _db.Product.Where(p=>p.ProductId==brand.ProductId).FirstOrDefaultAsync();

                SellUnitPrice =Convert.ToDouble(CountSellunitPrice(item.CategoryId));
                stockAvailabe = Convert.ToDouble(StockRemainingQuantity(item.CategoryId));

                VmCategoryAndStockProduct vm = new VmCategoryAndStockProduct()
                {
                     CategoryId =item.CategoryId ,
                     CategoryName =item.CategoryName ,

                    BrandId =brand.BrandId ,
                    BrandName =brand.BrandName ,

                    ProductId =product.ProductId ,
                    ProductName =product.ProductName ,

                    StockAvailable = stockAvailabe,

                    UnitPrice = SellUnitPrice,
                };
                vmList.Add(vm);
            }


            return vmList;
        }

        public double StockRemainingQuantity(int id)   //id means categoryId
        {
            double PurchaseQuantity = 0;
            double SellQuantity = 0;
            double StockRemainingQuantity = 0;

            var productCostList =  _db.ProductEntry.Where(pc => pc.CategoryId == id).ToList();

            foreach (var item in productCostList)
            {
                PurchaseQuantity += item.Quantity;
            }

            var productSellList = _db.Product_Sell.Where(ps => ps.CategoryId == id).ToList();

            foreach (var item in productSellList)
            {
                SellQuantity += item.Quantity;
            }
            StockRemainingQuantity = Convert.ToDouble(PurchaseQuantity - SellQuantity);
            return StockRemainingQuantity;

        }

        private double CountSellunitPrice(int id) //id means CategoryId
        {
            double totalQuantity = 0;
            double totalCost = 0;
            double avgCostUnitPrice = 0;
            double avgSellUniPrice =0;
            double ProfitPercentage = 10.0;

            
            var CategoryWiseProductCostList =  _db.ProductEntry.Where(c => c.CategoryId ==id).ToList();
            if (CategoryWiseProductCostList!=null)
            {
                foreach (var data in CategoryWiseProductCostList)
                {
                    totalQuantity += data.Quantity;
                    totalCost += data.SubTotalCost;
                };

                if (totalQuantity > 0)
                {
                    avgCostUnitPrice = Convert.ToDouble(totalCost / totalQuantity);

                    avgSellUniPrice = Convert.ToDouble((avgCostUnitPrice*(ProfitPercentage+100))/100);
                }

            }
            
            return avgSellUniPrice;
        }


        // GET: api/Product_Sell/GetProduct_SellList
        [HttpGet]
        [ActionName("GetProduct_SellList")]
        public async Task<ActionResult<IEnumerable<VmProductSellDetails>>> GetProduct_SellList()
        {
            List<VmProductSellDetails> vmList = new List<VmProductSellDetails>();

            var productCostList = await _db.Product_Sell.ToListAsync();

            foreach (var data in productCostList)
            {

                var emp = await _db.Employee.Where(e => e.EmployeeId == data.EmployeeId).Select(e => new { e.EmpAddress, e.EmployeeId, e.EmpMobileNo, e.EmpName, e.GenderId, e.ReligionId, e.ThanaId }).FirstOrDefaultAsync();
                var category = await _db.Category.Where(c => c.CategoryId == data.CategoryId).Select(c => new { c.CategoryId, c.CategoryName, c.BrandId }).FirstOrDefaultAsync();
                var brand = await _db.Brand.Where(b => b.BrandId == category.BrandId).Select(b => new { b.BrandId, b.BrandName, b.ProductId }).FirstOrDefaultAsync();
                var product = await _db.Product.Where(p => p.ProductId == brand.ProductId).Select(p => new { p.ProductId, p.ProductName }).FirstOrDefaultAsync();
                var customer = await _db.Customer.Where(cs => cs.CustomerId == data.CustomerId).Select(c => new { c.CustomerId, c.Address, c.CustomerMobileNo, c.CustomerName }).FirstOrDefaultAsync();
                VmProductSellDetails vm = new VmProductSellDetails()
                {

                    Product_SellId = data.Product_SellId,
                    Quantity = data.Quantity,
                    UnitPrice = data.UnitPrice,
                    Discount_Percentage = data.Discount_Percentage,
                    SubTotalCost = data.SubTotalCost,
                    DateOfSell = data.DateOfSell,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    EmployeeId = emp.EmployeeId,
                    EmpName = emp.EmpName,
                    CustomerId= customer.CustomerId,
                    CustomerMobileNo=customer.CustomerMobileNo,
                    CustomerName=customer.CustomerName
                    
                };
                vmList.Add(vm);
            }


            return vmList;
        }

        [HttpGet]
        [ActionName("Today_Product_SellList")]
        public async Task<ActionResult<IEnumerable<VmProductSellDetails>>> Today_Product_SellList()
        {
            List<VmProductSellDetails> vmList = new List<VmProductSellDetails>();

            var productCostList = await _db.Product_Sell.Where(cp => cp.DateOfSell.Year == DateTime.Now.Year && cp.DateOfSell.Month == DateTime.Now.Month && cp.DateOfSell.Day == DateTime.Now.Day).ToListAsync();

            foreach (var data in productCostList)
            {

                var emp = await _db.Employee.Where(e => e.EmployeeId == data.EmployeeId).Select(e => new { e.EmpAddress, e.EmployeeId, e.EmpMobileNo, e.EmpName, e.GenderId, e.ReligionId, e.ThanaId }).FirstOrDefaultAsync();
                var category = await _db.Category.Where(c => c.CategoryId == data.CategoryId).Select(c => new { c.CategoryId, c.CategoryName, c.BrandId }).FirstOrDefaultAsync();
                var brand = await _db.Brand.Where(b => b.BrandId == category.BrandId).Select(b => new { b.BrandId, b.BrandName, b.ProductId }).FirstOrDefaultAsync();
                var product = await _db.Product.Where(p => p.ProductId == brand.ProductId).Select(p => new { p.ProductId, p.ProductName }).FirstOrDefaultAsync();
                var customer = await _db.Customer.Where(cs => cs.CustomerId == data.CustomerId).Select(c => new { c.CustomerId, c.Address, c.CustomerMobileNo, c.CustomerName }).FirstOrDefaultAsync();
                VmProductSellDetails vm = new VmProductSellDetails()
                {

                    Product_SellId = data.Product_SellId,
                    Quantity = data.Quantity,
                    UnitPrice = data.UnitPrice,
                    Discount_Percentage = data.Discount_Percentage,
                    SubTotalCost = data.SubTotalCost,
                    DateOfSell = data.DateOfSell,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    EmployeeId = emp.EmployeeId,
                    EmpName = emp.EmpName,
                    CustomerId = customer.CustomerId,
                    CustomerMobileNo = customer.CustomerMobileNo,
                    CustomerName = customer.CustomerName

                };
                vmList.Add(vm);
            }


            return vmList;
        }


        [HttpPost]
        [ActionName("Monthly_Product_SellList")]
        public async Task<ActionResult<IEnumerable<VmProductSellDetails>>> Monthly_Product_SellList(VmSearchParams searchParams)
        {
            List<VmProductSellDetails> vmList = new List<VmProductSellDetails>();
            List<Product_Sell> productSellList = new List<Product_Sell>();

          var dataList=  await _db.Product_Sell.Where(cp => cp.DateOfSell.Year ==searchParams.DateOfSell.Year  && cp.DateOfSell.Month ==searchParams.DateOfSell.Month).ToListAsync();
            foreach (var data in dataList)
            {
                Product_Sell psel = new Product_Sell()
                {
                    Product_SellId = data.Product_SellId,
                    Quantity =data.Quantity ,
                    UnitPrice =data.UnitPrice ,
                    Discount_Percentage =data.Discount_Percentage ,
                    SubTotalCost =data.SubTotalCost ,
                    DateOfSell =data.DateOfSell ,
                    CategoryId =data.CategoryId ,
                    EmployeeId =data.EmployeeId ,
                    CustomerId =data.CustomerId 
                };

                productSellList.Add(psel);
            }

            foreach (var data in productSellList)
            {

                var emp = await _db.Employee.Where(e => e.EmployeeId == data.EmployeeId).Select(e => new { e.EmpAddress, e.EmployeeId, e.EmpMobileNo, e.EmpName, e.GenderId, e.ReligionId, e.ThanaId }).FirstOrDefaultAsync();
                var category = await _db.Category.Where(c => c.CategoryId == data.CategoryId).Select(c => new { c.CategoryId, c.CategoryName, c.BrandId }).FirstOrDefaultAsync();
                var brand = await _db.Brand.Where(b => b.BrandId == category.BrandId).Select(b => new { b.BrandId, b.BrandName, b.ProductId }).FirstOrDefaultAsync();
                var product = await _db.Product.Where(p => p.ProductId == brand.ProductId).Select(p => new { p.ProductId, p.ProductName }).FirstOrDefaultAsync();
                var customer = await _db.Customer.Where(cs => cs.CustomerId == data.CustomerId).Select(c => new { c.CustomerId, c.Address, c.CustomerMobileNo, c.CustomerName }).FirstOrDefaultAsync();
                VmProductSellDetails vm = new VmProductSellDetails()
                {

                    Product_SellId = data.Product_SellId,
                    Quantity = data.Quantity,
                    UnitPrice = data.UnitPrice,
                    Discount_Percentage = data.Discount_Percentage,
                    SubTotalCost = data.SubTotalCost,
                    DateOfSell = data.DateOfSell,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    EmployeeId = emp.EmployeeId,
                    EmpName = emp.EmpName,
                    CustomerId = customer.CustomerId,
                    CustomerMobileNo = customer.CustomerMobileNo,
                    CustomerName = customer.CustomerName

                };
                vmList.Add(vm);
            }


            return vmList;
        }


        // GET: api/Product_Sell/GetProduct_Sell/5
        [HttpGet("{id}")]
        [ActionName("GetProduct_Sell")]
        public async Task<ActionResult<Product_Sell>> GetProduct_Sell(int id)
        {
            var data = await _db.Product_Sell.Where(ps=>ps.Product_SellId==id).Select(s=>new {s.CategoryId,s.CustomerId,s.DateOfSell,s.Discount_Percentage,s.EmployeeId,s.Product_SellId,s.Quantity,s.SubTotalCost,s.UnitPrice }).FirstOrDefaultAsync();

            Product_Sell editProductSellObject = new Product_Sell()
            {
                Product_SellId =data.Product_SellId,
                CategoryId =data.CustomerId,
                CustomerId=data.CustomerId,
                EmployeeId=data.EmployeeId,
                Quantity=data.Quantity,
                UnitPrice=data.UnitPrice,
                Discount_Percentage=data.Discount_Percentage,
                SubTotalCost=data.SubTotalCost,
                DateOfSell=data.DateOfSell
                
            };


            if (editProductSellObject == null)
            {
                return NotFound();
            }

            return editProductSellObject;
        }

        // PUT: api/Product_Sell/PutProduct_Sell/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutProduct_Sell")]
        public async Task<ActionResult<string>> PutProduct_Sell(int id, Product_Sell editSell)
        {
            if (id != editSell.Product_SellId)
            {
                return BadRequest();
            }

            double subTotal = 0;
            subTotal = Convert.ToDouble((editSell.Quantity * editSell.UnitPrice) - editSell.Discount_Percentage);
            string msg = null;

            if (editSell.Discount_Percentage <= 0)
            {
                editSell.Discount_Percentage = 0;
            }

            Product_Sell EditSellProduct = new Product_Sell()
            {
                Product_SellId = editSell.Product_SellId,
                Quantity = editSell.Quantity,

                UnitPrice = editSell.UnitPrice,

                Discount_Percentage = editSell.Discount_Percentage,
                SubTotalCost = subTotal,

                DateOfSell = Convert.ToDateTime(DateTime.Now),
                CategoryId = editSell.CategoryId,
                EmployeeId = editSell.EmployeeId,
                CustomerId = editSell.CustomerId,


            };



            _db.Entry(EditSellProduct).State = EntityState.Modified;

            try
            {
               int editResponse =await _db.SaveChangesAsync();

                if (editResponse>0)
                {
                    msg = "Updated Record Successfully!!";
                }
                else
                {
                    msg = "update operation Unsuccessfull";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_SellExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return msg;
        }

        // POST: api/Product_Sell/PostProduct_Sell
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostProduct_Sell")]
        public async Task<ActionResult<string>> PostProduct_Sell(Product_Sell pSell)
        {
            double subTotal = 0;
            subTotal = Convert.ToDouble((pSell.Quantity * pSell.UnitPrice) - pSell.Discount_Percentage);
            string msg = null;

            if (pSell.Discount_Percentage<=0)
            {
                pSell.Discount_Percentage = 0;
            }

            Product_Sell SellProduct = new Product_Sell()
            {
                Product_SellId = 0,
                Quantity = pSell.Quantity,

                UnitPrice = pSell.UnitPrice,

                Discount_Percentage = pSell.Discount_Percentage,
                SubTotalCost = subTotal,

                DateOfSell = Convert.ToDateTime(DateTime.Now),
                CategoryId = pSell.CategoryId,
                EmployeeId = pSell.EmployeeId,
                CustomerId = pSell.CustomerId,

                
            };





            _db.Product_Sell.Add(SellProduct);
          int response = await _db.SaveChangesAsync();

    

            if (response>0)
            {
                msg = "Inserted Record Successfully!!";
            }
            else
            {
                msg = "Invalid data";
            }


            return msg;
        }

        // DELETE: api/Product_Sell/DeleteProduct_Sell/5
        [HttpDelete("{id}")]
        [ActionName("DeleteProduct_Sell")]
        public async Task<ActionResult<string>> DeleteProduct_Sell(int id)
        {
            string msg = null;
            
            var product_Sell = await _db.Product_Sell.FindAsync(id);
            if (product_Sell == null)
            {
                return NotFound();
            }

            _db.Product_Sell.Remove(product_Sell);
          int response = await _db.SaveChangesAsync();
            if (response>0)
            {
                msg = "Deleted Record Successfully!!";
            }
            else
            {
                msg = "Record can not delete";
            }
            return msg;
        }




        private bool Product_SellExists(int id)
        {
            return _db.Product_Sell.Any(e => e.Product_SellId == id);
        }



    }
}

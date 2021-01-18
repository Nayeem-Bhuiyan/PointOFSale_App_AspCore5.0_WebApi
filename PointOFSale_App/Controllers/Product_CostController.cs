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
    public class ProductEntryController : ControllerBase
    {
        private readonly PointOfSellDbContext _db;

        public ProductEntryController(PointOfSellDbContext db)
        {
            _db = db;
        }

        // GET: api/ProductEntry/GetProductEntryList
        [HttpGet]
        [ActionName("GetProductEntryList")]
        public async Task<ActionResult<IEnumerable<VmProductCostDetails>>> GetProductEntryList()
        {
            List<VmProductCostDetails> vmList = new List<VmProductCostDetails>();

           var productCostList= await _db.ProductEntry.ToListAsync();

            foreach (var data in productCostList)
            {

             var emp=   await _db.Employee.Where(e => e.EmployeeId == data.EmployeeId).Select(e=>new {e.EmpAddress,e.EmployeeId,e.EmpMobileNo,e.EmpName,e.GenderId,e.ReligionId,e.ThanaId }).FirstOrDefaultAsync();
             var category = await _db.Category.Where(c => c.CategoryId == data.CategoryId).Select(c=>new { c.CategoryId,c.CategoryName,c.BrandId}).FirstOrDefaultAsync();
             var brand = await _db.Brand.Where(b => b.BrandId == category.BrandId).Select(b=>new {b.BrandId,b.BrandName,b.ProductId }).FirstOrDefaultAsync();
             var product = await _db.Product.Where(p => p.ProductId == brand.ProductId).Select(p => new { p.ProductId, p.ProductName }).FirstOrDefaultAsync();

             


                VmProductCostDetails vm = new VmProductCostDetails()
                {
                            
                    ProductEntryId = data.ProductEntryId,
                    VoucherNumber=data.VoucherNumber,
                    Quantity =data.Quantity ,
                    UnitPrice =data.UnitPrice ,
                    Discount_Percentage =data.Discount_Percentage ,
                    SubTotalCost =data.SubTotalCost ,
                    DateOfEntry = data.DateOfEntry,
                    CategoryId =category.CategoryId ,
                    CategoryName =category.CategoryName ,
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    EmployeeId =emp.EmployeeId ,
                    EmpName =emp.EmpName ,
                };
                vmList.Add(vm);
            }
            
            
            return vmList;
        }

        // GET: api/ProductEntry/GetProductEntry/5
        [HttpGet("{id}")]
        [ActionName("GetProductEntry")]
        public async Task<ActionResult<ProductEntry>> GetProductEntry(int id)
        {
            var ProductEntry = await _db.ProductEntry.FindAsync(id);

            if (ProductEntry == null)
            {
                return NotFound();
            }

            return ProductEntry;
        }

        // PUT: api/ProductEntry/PutProductEntry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutProductEntry")]
        public async Task<ActionResult<string>> PutProductEntry(int id, ProductEntry editObj)
        {
            if (id != editObj.ProductEntryId)
            {
                return BadRequest();
            }

            double sTotal = 0;
            sTotal = Convert.ToDouble((editObj.Quantity * editObj.UnitPrice) - editObj.Discount_Percentage);
            string msg = null;


            ProductEntry Pentry = new ProductEntry()
            {
                ProductEntryId = 0,
                VoucherNumber = editObj.VoucherNumber,
                Quantity = editObj.Quantity,
                UnitPrice = editObj.UnitPrice,
                Discount_Percentage = editObj.Discount_Percentage,
                SubTotalCost = sTotal,
                DateOfEntry = DateTime.Now,
                CategoryId = editObj.CategoryId,
                EmployeeId = editObj.EmployeeId


            };


            _db.Entry(Pentry).State = EntityState.Modified;
             int response = await _db.SaveChangesAsync();

                if (response > 0)
                {
                    
                    msg = "Updated Record Successfully!!";
                }
                else
                {
                    msg = "Invalid Record can not Updated";
                }
            
          
            return msg;
        }

        // POST: api/ProductEntry/PostProductEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostProductEntry")]
        public async Task<ActionResult<string>> PostProductEntry(ProductEntry obj)
        {
            double sTotal = 0;
            sTotal = Convert.ToDouble((obj.Quantity* obj.UnitPrice)- obj.Discount_Percentage);
            string msg = null;
            
            
            ProductEntry Pentry = new ProductEntry()
            {
                 ProductEntryId =0 ,
                 VoucherNumber = obj.VoucherNumber,
                 Quantity =obj.Quantity ,
                 UnitPrice =obj.UnitPrice ,
                 Discount_Percentage =obj.Discount_Percentage ,
                 SubTotalCost = sTotal,
                 DateOfEntry =DateTime.Now ,
                 CategoryId =obj.CategoryId ,
                 EmployeeId =obj.EmployeeId 


            };


            _db.ProductEntry.Add(Pentry);
         int response  = await _db.SaveChangesAsync();
            if (response>0)
            {
                msg = "Inserted Record Successfully!!";
            }
            else
            {
                msg = "Invalid Record can not Saved";
            }

            return msg;
        }

        // DELETE: api/ProductEntry/DeleteProductEntry/5
        [HttpDelete("{id}")]
        [ActionName("DeleteProductEntry")]
        public async Task<ActionResult<string>> DeleteProductEntry(int id)
        {
            string msg = null;
            
            var ProductEntry = await _db.ProductEntry.FindAsync(id);
            if (ProductEntry == null)
            {
                return NotFound();
            }

            _db.ProductEntry.Remove(ProductEntry);
          int response  =await _db.SaveChangesAsync();
            if (response>0)
            {
                msg = "Deleted record Successfully!!";
            }
            else
            {
                msg = "Sorry record can not delete";
            }
            return msg;
        }

        private bool ProductEntryExists(int id)
        {
            return _db.ProductEntry.Any(e => e.ProductEntryId == id);
        }
    }
}

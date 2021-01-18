using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOFSale_App.Models;
using PointOFSale_App.ViewModel;

namespace PointOFSale_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockCount : ControllerBase
    {
        public PointOfSellDbContext db;
        public StockCount(PointOfSellDbContext _db)
        {
            db = _db;
        }

        //GET:/api/StockCount/StockRemainingQuantity/1

        [HttpGet("{id}")]
        [ActionName("StockRemainingQuantity")]
        public async Task<ActionResult<double>> StockRemainingQuantity(int id)   //id=categoryId
        {
            double PurchaseQuantity = 0;
            double SellQuantity = 0;
            double StockRemainingQuantity = 0;

          var productCostList  =await db.ProductEntry.Where(pc => pc.CategoryId == id).ToListAsync();

            foreach (var item in productCostList)
            {
                PurchaseQuantity += item.Quantity;
            }


            var productSellList = await db.Product_Sell.Where(ps => ps.CategoryId == id).ToListAsync();

            foreach (var item in productSellList)
            {
                SellQuantity += item.Quantity;
            }


            StockRemainingQuantity = Convert.ToDouble(PurchaseQuantity - SellQuantity);

            return StockRemainingQuantity;

        }

        //this method will show all product list which Stock Quantiy is less then 50
        //GET:/api/StockCount/ShortStockProductList
        [HttpGet]
        [ActionName("ShortStockProductList")]
        public async Task<ActionResult<IEnumerable<VmShortStock_ProductList>>> ShortStockProductList()
        {
            List<VmShortStock_ProductList> vmShortProductList = new List<VmShortStock_ProductList>();

           var ProductCategoryList= await db.Category.ToListAsync();
            double totalEntryQuantity = 0;
            double totalSellQuantity = 0;
            double totalStockRemaingQuantity = 0;


            foreach (var data in ProductCategoryList)
            {
               var ProductCostList= await db.ProductEntry.Where(pe=>pe.CategoryId==data.CategoryId).ToListAsync();

                foreach (var item in ProductCostList)
                {
                    totalEntryQuantity += item.Quantity;
                }

               var ProductSellList= await db.Product_Sell.Where(ps=>ps.CategoryId==data.CategoryId).ToListAsync();

                foreach (var item in ProductSellList)
                {
                    totalSellQuantity += item.Quantity;
                }

                if (totalEntryQuantity> totalSellQuantity)
                {
                    totalStockRemaingQuantity= Convert.ToDouble(totalEntryQuantity - totalSellQuantity);
                }
                else if (totalEntryQuantity == totalSellQuantity)
                {
                    totalStockRemaingQuantity = 0;
                }
                else
                {
                    totalStockRemaingQuantity = 0;
                }

                var brand = await db.Brand.Where(b=>b.BrandId==data.BrandId).FirstOrDefaultAsync();
                var product = await db.Product.Where(p=>p.ProductId==brand.ProductId).FirstOrDefaultAsync();

                if (totalStockRemaingQuantity<50)  
                {
                    VmShortStock_ProductList shortProduct = new VmShortStock_ProductList()
                    {
                        ProductId=product.ProductId,
                        ProductName=product.ProductName,
                        BrandId=brand.BrandId,
                        BrandName=brand.BrandName,
                        CategoryId=data.CategoryId,
                        CategoryName=data.CategoryName,
                        TotalEntryQuantity=totalEntryQuantity,
                        TotalSellQuantity=totalSellQuantity,
                        TotalStockRemainingQuantity=totalStockRemaingQuantity
                    };
                    vmShortProductList.Add(shortProduct);
                }
                totalEntryQuantity = 0;
                totalSellQuantity = 0;
                totalStockRemaingQuantity = 0;

            }



            return vmShortProductList;
        }



    }
}

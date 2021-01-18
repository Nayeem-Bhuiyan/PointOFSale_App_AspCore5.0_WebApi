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
    public class Profit : ControllerBase
    {
        private readonly PointOfSellDbContext db;
        public Profit(PointOfSellDbContext _db)
        {
            this.db = _db;
        }

        [HttpGet]
        [ActionName("ProfitListDetails")]
        public async Task<ActionResult<IEnumerable<VmProfitCountDetails>>> ProfitListDetails()
        {
            List<VmProfitCountDetails> ProfitList = new List<VmProfitCountDetails>();
            var categoryList = await db.Category.ToListAsync();

            double Purchase_TotalQuantity = 0;
            double Purchase_TotalPrice = 0;
            double Purchase_AveragePrice = 0;

            double Sell_TotalQuantity = 0;
            double Sell_TotalPrice = 0;
            double Sell_AveragePrice = 0;

            double totalProfit = 0;

            double grandTotalProfit = 0;

            foreach (var category in categoryList)
            {

                var productCostList_ByCategoryId = await db.ProductEntry.Where(c=>c.CategoryId==category.CategoryId).ToListAsync();
                var productSellList_ByCategoryId = await db.Product_Sell.Where(s => s.CategoryId == category.CategoryId).ToListAsync();


                foreach (var item in productCostList_ByCategoryId)
                {
                     Purchase_TotalQuantity +=item.Quantity;
                     Purchase_TotalPrice += item.SubTotalCost;
                   
                }
                 Purchase_AveragePrice = Convert.ToDouble(Purchase_TotalPrice/Purchase_TotalQuantity);

                foreach (var item in productSellList_ByCategoryId)
                {
                     Sell_TotalQuantity +=item.Quantity;
                     Sell_TotalPrice +=item.SubTotalCost;
                    
                }
                 Sell_AveragePrice =Convert.ToDouble(Sell_TotalPrice/Sell_TotalQuantity);

                totalProfit = Convert.ToDouble(Sell_TotalQuantity*(Sell_AveragePrice- Purchase_AveragePrice));
                grandTotalProfit += totalProfit;
                VmProfitCountDetails vm = new VmProfitCountDetails()
                {
                    CategotyId=category.CategoryId,
                    CategoryName=category.CategoryName,
                    Average_CostUnitPrice=Purchase_AveragePrice,
                    Average_SellUnitPrice=Sell_AveragePrice,
                    Total_SellQuantity=Sell_TotalQuantity,
                    TotalProfitOfThisProduct = totalProfit,
                    
                };

               ProfitList.Add(vm);
            }


            return ProfitList;
        }



        [HttpGet]
        [ActionName("GrandTotalProfitCount")]
        public async Task<ActionResult<double>> GrandTotalProfitCount()
        {
            
            var categoryList = await db.Category.ToListAsync();

            double Purchase_TotalQuantity = 0;
            double Purchase_TotalPrice = 0;
            double Purchase_AveragePrice = 0;

            double Sell_TotalQuantity = 0;
            double Sell_TotalPrice = 0;
            double Sell_AveragePrice = 0;

            double totalProfit = 0;

            double grandTotalProfit = 0;

            foreach (var category in categoryList)
            {

                var productCostList_ByCategoryId = await db.ProductEntry.Where(c => c.CategoryId == category.CategoryId).ToListAsync();
                var productSellList_ByCategoryId = await db.Product_Sell.Where(s => s.CategoryId == category.CategoryId).ToListAsync();


                foreach (var item in productCostList_ByCategoryId)
                {
                    Purchase_TotalQuantity += item.Quantity;
                    Purchase_TotalPrice += item.SubTotalCost;

                }
                Purchase_AveragePrice = Convert.ToDouble(Purchase_TotalPrice / Purchase_TotalQuantity);

                foreach (var item in productSellList_ByCategoryId)
                {
                    Sell_TotalQuantity += item.Quantity;
                    Sell_TotalPrice += item.SubTotalCost;

                }
                Sell_AveragePrice = Convert.ToDouble(Sell_TotalPrice / Sell_TotalQuantity);

                totalProfit = Convert.ToDouble(Sell_TotalQuantity * (Sell_AveragePrice - Purchase_AveragePrice));
                grandTotalProfit += Convert.ToDouble(totalProfit);
            }


            return grandTotalProfit;
        }












    }
}

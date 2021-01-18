using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PointOFSale_App.ViewModel
{
    public class VmShortStock_ProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public double TotalEntryQuantity { get; set; }
        public double TotalSellQuantity { get; set; }
        public double TotalStockRemainingQuantity { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmCategoryAndStockProduct
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public double  StockAvailable { get; set; }

        public double UnitPrice { get; set; }
    }
}

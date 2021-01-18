using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOFSale_App.ViewModel
{
    public class VmProfitCountDetails
    {
        public int VmProfitCountDetailsId { get; set; }

        //CategoryTable_Informations
        public int CategotyId { get; set; } //CategotyId
        public string CategoryName { get; set; }  //CategoryName
        public byte ByteImage { get; set; }
        public String ImagePath { get; set; }

        //OthersInformations
        public double Average_CostUnitPrice { get; set; }
        public double Average_SellUnitPrice { get; set; }
        public double Total_SellQuantity { get; set; }
        public double TotalProfitOfThisProduct { get; set; }
        

    }
}

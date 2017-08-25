using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinksSale.Models
{
    public class SalesManagementModel
    {
        public List<ProductModel> Products { get; set; }

        public List<CoinModel> Coins { get; set; }
    }
}
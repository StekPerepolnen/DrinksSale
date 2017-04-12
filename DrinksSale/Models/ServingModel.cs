using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DrinksSale.Database;

namespace DrinksSale.Models
{
    public class ServingModel
    {
        public List<ProductModel> Products { get; set; }

        public List<CoinModel> Coins { get; set; }

        public int Inserted { get; set; }
    }
}
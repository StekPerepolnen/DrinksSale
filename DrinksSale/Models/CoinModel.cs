using DrinksSale.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinksSale.Models
{
    public class CoinModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        [Display(Name = "Запас")]
        public int Reserve { get; set; }

        public int Inserted { get; set; }

        public string Image { get; set; }

        [Display(Name = "Блокировать")]
        public bool IsBlocked { get; set; }

        public CoinModel()
        {

        }

        public CoinModel(Coin coin)
        {
            Id = coin.Id;
            Name = coin.Name;
            Value = coin.Value;
            Reserve = coin.Reserve;
            Inserted = coin.Inserted;
            Image = coin.Image;
            IsBlocked = coin.IsBlocked;
        }
    }
}
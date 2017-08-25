using DrinksSale.Database;
using DrinksSale.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrinksSale.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Кол-во")]
        public int Count { get; set; }

        [Display(Name = "Стоимость")]
        public int Value { get; set; }

        public string Image { get; set; }

        public int Ordered { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(Drink drink)
        {
            Id = drink.Id;
            Name = drink.Name;
            Count = drink.Count;
            Value = drink.Value;
            Image = drink.Image;
            Ordered = 0;
        }
    }
}
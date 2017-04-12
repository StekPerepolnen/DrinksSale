using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinksSale.Models
{
    public class DeliveryEntityModel
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public DeliveryEntityModel(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
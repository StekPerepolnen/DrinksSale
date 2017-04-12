using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinksSale.Models;
using DrinksSale.Database;
using System.IO;

namespace DrinksSale.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ServingModel();

            model.Products = Repository.GetDrinksList().Where(x => x.Value > 0).ToList().Select(x => new ProductModel(x)).ToList();
            model.Coins = Repository.GetCoinsList().OrderBy(x => x.Value).ToList().Select(x => new CoinModel(x)).ToList();
            model.Inserted = model.Coins.Sum(x => x.Value * x.Inserted);

            return View(model);
        }

        public ActionResult InsertCoin(int id)
        {
            var coin = Repository.GetCoin(id);

            if (coin != null)
            {
                coin.Inserted++;
                Repository.SaveCoin(coin);
            }

            return null;
        }

        public ActionResult Buy(Order[] cart)
        {
            var delivery = new DeliveryModel();

            var inserted = Repository.GetCoinsList().Sum(x => x.Value * x.Inserted);
            var payment = 0;

            for (int i = 0; i < cart.Count(); i++)
            {
                var drink = Repository.GetDrink(cart[i].id);
                if (drink == null || cart[i].number > drink.Count)
                    return null;

                payment += drink.Value * cart[i].number;
            }

            var rest = inserted - payment;
            if (rest < 0)
                return null;

            for (int i = 0; i < cart.Count(); i++)
            {
                var drink = Repository.GetDrink(cart[i].id);

                drink.Count -= cart[i].number;
                delivery.Products.Add(new DeliveryEntityModel(drink.Name, cart[i].number));

                Repository.SaveDrink(drink);
            }

            var coins = Repository.GetCoinsList().OrderByDescending(x => x.Value).ToList();
            coins.ForEach(x =>
            {
                x.Reserve += x.Inserted;
                x.Inserted = 0;

                var needs = rest / x.Value;
                var min = Math.Min(needs, x.Reserve);
                x.Reserve -= min;
                rest -= min * x.Value;

                if (min > 0)
                    delivery.Coins.Add(new DeliveryEntityModel(x.Name, min));
            });

            return Json(delivery, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinksSale.Models;
using System.IO;
using DrinksSale.Database;

namespace DrinksSale.Controllers
{
    [SecretKeyFilterAttribute(Parameter = "key", Value = "asdf")]
    public class AdminController : BaseController
    {

        public ActionResult Index()
        {
            var model = new ServingModel();

            model.Products = Repository.GetDrinksList().Select(x => new ProductModel(x)).ToList();
            model.Coins = Repository.GetCoinsList().OrderBy(x => x.Value).ToList().Select(x => new CoinModel(x)).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel product)
        {
            ProductModel model = null;

            var drink = Repository.GetDrink(product.Id);
            if (drink == null)
                return null;

            drink.Name = product.Name;
            drink.Count = product.Count;
            drink.Value = product.Value;

            Repository.SaveDrink(drink);

            model = new ProductModel(drink);

            return PartialView("ProductDataPartial", model);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int Id)
        {
            Repository.RemoveDrink(Id);

            return null;
        }

        [HttpPost]
        public ActionResult UploadImage(int Id)
        {
            ProductModel model = null;

            var drink = Repository.GetDrink(Id);
            if (drink == null)
                return null;

            if (Request.Files.Count > 0)
            {

                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Product/"), Id + fileName);
                    file.SaveAs(path);

                    drink.Image = Path.Combine("~/Images/Product/", Id + fileName);
                }
            }

            Repository.SaveDrink(drink);

            model = new ProductModel(drink);

            return PartialView("ProductImagePartial", model);
        }

        [HttpPost]
        public ActionResult CreateProduct()
        {
            ProductModel model = null;
            
                var drink = new Drink()
                {
                    Name = "Напиток",
                    Count = 0,
                    Value = 0
                };

                Repository.SaveDrink(drink);
                model = new ProductModel(drink);

            return PartialView("ProductPartial", model);
        }

        [HttpPost]
        public ActionResult UpdateCoin(CoinModel model)
        {
            var coin = Repository.GetCoin(model.Id);
            if (coin == null)
                return null;


            coin.Reserve = model.Reserve;
            coin.IsBlocked = model.IsBlocked;
            Repository.SaveCoin(coin);

            model = new CoinModel(coin);

            return PartialView("CoinPartial", model);
        }
    }
}
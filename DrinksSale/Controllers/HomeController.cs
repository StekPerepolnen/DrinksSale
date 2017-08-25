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
            var model = DataProvider.GetServingModel();

            return View(model);
        }

        public ActionResult InsertCoin(int id)
        {
            DataProvider.InsertCoin(id);

            return null;
        }

        public ActionResult Buy(Order[] cart)
        {
            var delivery = DataProvider.Order(cart);

            return Json(delivery, JsonRequestBehavior.AllowGet);
        }
    }
}
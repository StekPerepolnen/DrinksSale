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
            var model = DataProvider.GetSalesManagementModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel product)
        {
            ProductModel model = DataProvider.UpdateProduct(product);

            return PartialView("ProductDataPartial", model);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int Id)
        {
            DataProvider.RemoveDrink(Id);

            return null;
        }

        [HttpPost]
        public ActionResult UploadImage(int Id)
        {
            string fileName = SaveUploadedProductImage(Id);

            var model = DataProvider.UpdateProductImage(Id, fileName);

            return PartialView("ProductImagePartial", model);
        }

        private string SaveUploadedProductImage(int Id)
        {
            string fileName = null;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/Product/"), Id + extension);
                    file.SaveAs(path);
                    fileName = Path.Combine("~/Images/Product/", Id + extension);
                }
            }

            return fileName;
        }

        [HttpPost]
        public ActionResult CreateProduct()
        {
            ProductModel model = DataProvider.CreateEmptyProduct();

            return PartialView("ProductPartial", model);
        }

        [HttpPost]
        public ActionResult UpdateCoin(CoinModel model)
        {
            var coin = DataProvider.UpdateCoinModel(model);

            return PartialView("CoinPartial", model);
        }
    }
}
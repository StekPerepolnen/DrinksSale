using DrinksSale.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinksSale.Controllers
{
    public class BaseController: Controller
    {
        private IRepository _repository;

        public IRepository Repository { get { return _repository; } }

        public BaseController()
        {
            _repository = new Repository();
        }
    }
}
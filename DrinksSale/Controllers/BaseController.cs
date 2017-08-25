using DrinksSale.Database;
using DrinksSale.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrinksSale.Controllers
{
    public class BaseController: Controller
    {
        private ISaleItemDataProvider _dataProvider;

        public ISaleItemDataProvider DataProvider { get { return _dataProvider; } }
        
        public void SetDataDirectoryOnSalesDb()
        {
            var appSetting = ConfigurationManager.AppSettings["SalesDbDir"];
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, appSetting);
            var fullPath = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", fullPath);
        }

        public void SetDefaultDataDirectory()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", fullPath);
        }

        public BaseController()
        {
            SetDataDirectoryOnSalesDb();

            _dataProvider = new SaleItemDataProvider(new SaleRepository());

           // SetDefaultDataDirectory();
        }
    }
}
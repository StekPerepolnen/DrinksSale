using DrinksSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinksSale.Database
{
    public interface ISaleItemDataProvider
    {
        ServingModel GetServingModel();

        SalesManagementModel GetSalesManagementModel();

        //Drink GetDrink(int id);

        //Drink SaveDrink(Drink instance);

        void RemoveDrink(int id);

        ProductModel CreateEmptyProduct();

        ProductModel UpdateProduct(ProductModel product);

        void InsertCoin(int id);

        CoinModel UpdateCoinModel(CoinModel coin);

        DeliveryModel Order(Order[] cart);

        ProductModel UpdateProductImage(int id, string fileName);
    }
}
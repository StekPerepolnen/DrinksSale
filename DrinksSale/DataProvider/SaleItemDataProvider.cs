using DrinksSale.Models;
using DrinksSale.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DrinksSale.Database
{
    public class SaleItemDataProvider: ISaleItemDataProvider
    {
        private ISaleRepository _saleRepository;

        public ISaleRepository SaleRepository {
            get { return _saleRepository; }
        }

        public SaleItemDataProvider(ISaleRepository repository)
        {
            _saleRepository = repository;
        }
        
        public ServingModel GetServingModel()
        {
            var model = new ServingModel();

            model.Products = SaleRepository.GetDrinksList().Where(x => x.Value > 0).ToList().Select(x => new ProductModel(x)).ToList();
            model.Coins = SaleRepository.GetCoinsList().OrderBy(x => x.Value).ToList().Select(x => new CoinModel(x)).ToList();
            model.Inserted = model.Coins.Sum(x => x.Value * x.Inserted);

            return model;
        }

        public SalesManagementModel GetSalesManagementModel()
        {
            var model = new SalesManagementModel();

            model.Products = SaleRepository.GetDrinksList().Select(x => new ProductModel(x)).ToList();
            model.Coins = SaleRepository.GetCoinsList().OrderBy(x => x.Value).ToList().Select(x => new CoinModel(x)).ToList();

            return model;
        }

        public void RemoveDrink(int id)
        {
            SaleRepository.RemoveDrink(id);
        }

        public ProductModel CreateEmptyProduct()
        {
            ProductModel model = null;

            var drink = new Drink()
            {
                Name = "Напиток",
                Count = 0,
                Value = 0
            };

            SaleRepository.SaveDrink(drink);
            model = new ProductModel(drink);

            return model;
        }

        public ProductModel UpdateProduct(ProductModel product)
        {
            ProductModel model = null;

            var drink = SaleRepository.GetDrink(product.Id);
            if (drink == null)
                return null;

            drink.Name = product.Name;
            drink.Count = product.Count;
            drink.Value = product.Value;

            SaleRepository.SaveDrink(drink);

            model = new ProductModel(drink);

            return model;
        }

        public void InsertCoin(int id)
        {
            var coin = SaleRepository.GetCoin(id);

            if (coin != null)
            {
                coin.Inserted++;
                SaleRepository.SaveCoin(coin);
            }
        }

        public CoinModel UpdateCoinModel(CoinModel model)
        {
            var coin = SaleRepository.GetCoin(model.Id);
            if (coin == null)
                return null;
            
            coin.Reserve = model.Reserve;
            coin.IsBlocked = model.IsBlocked;
            SaleRepository.SaveCoin(coin);

            return new CoinModel(coin);
        }

        public DeliveryModel Order(Order[] cart)
        {
            var delivery = new DeliveryModel();

            var inserted = SaleRepository.GetCoinsList().Sum(x => x.Value * x.Inserted);
            var payment = 0;

            for (int i = 0; i < cart.Count(); i++)
            {
                var drink = SaleRepository.GetDrink(cart[i].id);
                if (drink == null || cart[i].number > drink.Count)
                    return null;

                payment += drink.Value * cart[i].number;
            }

            var rest = inserted - payment;
            if (rest < 0)
                return null;

            for (int i = 0; i < cart.Count(); i++)
            {
                var drink = SaleRepository.GetDrink(cart[i].id);

                drink.Count -= cart[i].number;
                delivery.Products.Add(new DeliveryEntityModel(drink.Name, cart[i].number));

                SaleRepository.SaveDrink(drink);
            }

            var coins = SaleRepository.GetCoinsList().OrderByDescending(x => x.Value).ToList();
            coins.ForEach(x =>
            {
                x.Reserve += x.Inserted;
                x.Inserted = 0;

                var needs = rest / x.Value;
                var min = Math.Min(needs, x.Reserve);
                x.Reserve -= min;
                rest -= min * x.Value;
                SaleRepository.SaveCoin(x);

                if (min > 0)
                    delivery.Coins.Add(new DeliveryEntityModel(x.Name, min));
            });

            return delivery;
        }

        public ProductModel UpdateProductImage(int id, string fileName)
        {
            ProductModel model = null;

            var drink = SaleRepository.GetDrink(id);
            if (drink == null)
                return null;

            if (fileName != null)
            {
                drink.Image = fileName;
                SaleRepository.SaveDrink(drink);
            }

            model = new ProductModel(drink);

            return model;
        }
    }
}
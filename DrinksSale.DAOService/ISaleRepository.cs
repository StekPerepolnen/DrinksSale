using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinksSale.Service
{
    public interface ISaleRepository
    {
        List<Drink> GetDrinksList();

        Drink GetDrink(int id);

        Drink SaveDrink(Drink instance);

        void RemoveDrink(int id);

        List<Coin> GetCoinsList();

        Coin GetCoin(int id);

        Coin SaveCoin(Coin instance);
    }
}
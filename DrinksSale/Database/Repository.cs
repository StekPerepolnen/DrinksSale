using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DrinksSale.Database
{
    public class Repository: IRepository
    {
        #region Private

        private static void Db(Action<DbDrinksSaleEntities1> predicate)
        {
            using (var db = new DbDrinksSaleEntities1())
            {
                predicate.Invoke(db);
            }
        }

        private static TResult Db<TResult>(Func<DbDrinksSaleEntities1, TResult> predicate)
        {
            using (var db = new DbDrinksSaleEntities1())
            {
                return predicate.Invoke(db);
            }
        }
        #endregion Private

        public List<Drink> GetDrinksList()
        {
            return Db(_ => _.Drinks
                .ToList());
        }

        public Drink GetDrink(int id)
        {
            return Db(_ => _.Drinks.Find(id));
        }

        public Drink SaveDrink(Drink instance)
        {
            Db(_ =>
            {
                if (instance.Id == 0)
                {
                    _.Entry(instance).State = EntityState.Added;
                    _.SaveChanges();
                }
                else
                {
                    _.Entry(instance).State = EntityState.Modified;
                    _.SaveChanges();
                }

                return instance;
            });

            return instance;
        }

        public void RemoveDrink(int id)
        {
            Db(_ =>
            {
                var link = _.Drinks.Find(id);
                _.Entry(link).State = EntityState.Deleted;

                _.SaveChanges();
            });
        }

        public List<Coin> GetCoinsList()
        {
            return Db(_ => _.Coins
                .ToList());
        }

        public Coin GetCoin(int id)
        {
            return Db(_ => _.Coins.Find(id));
        }

        public Coin SaveCoin(Coin instance)
        {
            Db(_ =>
            {
                if (instance.Id == 0)
                {
                    _.Entry(instance).State = EntityState.Added;
                    _.SaveChanges();
                }
                else
                {
                    _.Entry(instance).State = EntityState.Modified;
                    _.SaveChanges();
                }

                return instance;
            });

            return instance;
        }
    }
}